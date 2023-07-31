﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Net.Http.Json;

namespace PlannerApp.Components
{
    public partial class LoginForm : ComponentBase
    {
        [Inject]
        public HttpClient ?HttpClient { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ILocalStorageService Storage { get; set; }

        private LoginRequest _model = new LoginRequest();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task LoginUserAsync()

        {
            _isBusy = true; 
            _errorMessage = string.Empty;

            var response = await HttpClient.PostAsJsonAsync("/api/v2/Auth/Login", _model);
            if (response.IsSuccessStatusCode) 
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>();
                //Store in local storage
                await Storage.SetItemAsStringAsync("access_token", result.Value.Token);
                await Storage.SetItemAsync<DateTime>("expiry_date", result.Value.ExpiryDate);

                await AuthenticationStateProvider.GetAuthenticationStateAsync();

                Navigation.NavigateTo("/");
                Console.WriteLine("Navigated");
                
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                _errorMessage = errorResult.Message;
            }


            _isBusy = false; 
        }

    }
}
