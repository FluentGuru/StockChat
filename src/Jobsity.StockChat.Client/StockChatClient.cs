using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Client
{
    public class StockChatClient
    {
        private readonly HttpClient httpClient;

        public StockChatClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Chat>> GetAllChats()
        {
            var response = await httpClient.GetAsync("stocks");
            return await response.Content.FromJsonContentAsync<IEnumerable<Chat>>();
        }

        public async Task<Chat> JoinChat(string stock)
        {
            var response = await httpClient.GetAsync($"stock/{stock}");
            return await response.Content.FromJsonContentAsync<Chat>();
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessages(string stock)
        {
            var response = await httpClient.GetAsync($"stock/{stock}/messages");
            return await response.Content.FromJsonContentAsync<IEnumerable<ChatMessage>>();
        }

        public async Task<IEnumerable<ChatParticipant>> GetChatParticipants(string stock)
        {
            var response = await httpClient.GetAsync($"stock/{stock}/participants");
            return await response.Content.FromJsonContentAsync<IEnumerable<ChatParticipant>>();
        }

        public async Task SendMessage(string stock, string message)
        {
            var response = await httpClient.PostAsync($"stock/{stock}/messages", new { message }.ToJsonContent());
        }

        public async Task<User> GetUser(string nickname)
        {
            var response = await httpClient.GetAsync($"users/{nickname}");
            switch(response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new UserNotFoundException();
                default:
                    return await response.Content.FromJsonContentAsync<User>();
            }
        }

        public Task<UserAuthentication> CreateUser(UserCredentials credentials)
        {
            return RunUserMutationCall("users", credentials);
        }

        public Task<UserAuthentication> AuthUser(UserCredentials credentials)
        {
            return RunUserMutationCall("users/auth", credentials);
        }

        public async Task EndAuth()
        {
            var response = await httpClient.DeleteAsync("users/auth");
            if(response.IsSuccessStatusCode)
            {
                ClearAuthenticationHeaders();
            }
        }

        private async Task<UserAuthentication> RunUserMutationCall(string resource, UserCredentials credentials)
        {
            var response = await httpClient.PostAsync(resource, credentials.ToJsonContent());

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new UserAuthenticationFailedException();
                case HttpStatusCode.InternalServerError:
                    throw new UserCreationException();
                default:
                    var authentication = await response.Content.FromJsonContentAsync<UserAuthentication>();
                    SetAuthenticationHeaders(authentication);
                    return authentication;
            }
        }

        private void SetAuthenticationHeaders(UserAuthentication authentication)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authToken", authentication.Token);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("nickname", authentication.User.Nickname);
        }

        private void ClearAuthenticationHeaders()
        {
            httpClient.DefaultRequestHeaders.Remove("authToken");
            httpClient.DefaultRequestHeaders.Remove("nickname");
        }
    }

    internal static class HttpContentExtensions
    {
        public static async Task<T> FromJsonContentAsync<T>(this HttpContent content)
        {
            var data = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static HttpContent ToJsonContent(this object content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }
    }
}
