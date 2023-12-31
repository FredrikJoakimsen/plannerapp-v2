﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Net.Http.Json;
using PlannerApp.Client.Services.Interfaces;
using System.Linq.Expressions;
using PlannerApp.Client.Services.Exeptions;

namespace PlannerApp.Components 
{ 
    public partial class RegisterForm
    {

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }


        

        private RegisterRequest _model = new RegisterRequest();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task RegisterUserAsync()

        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                await AuthenticationService.RegisterUserAsync(_model);
                Navigation.NavigateTo("/authentication/login");
            } 
            catch (ApiException ex) 
            {
                //Handle errors in api
                //TODO: log error and show gentle error message
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex) 
            {
                //Handle other errors
                _errorMessage = ex.Message;  
            }


            _isBusy = false;
        }


        private void RedirectToLogin()
        {
            Navigation.NavigateTo("/authentication/login");
        }


    }
}
