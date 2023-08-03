using PlannerApp.Client.Services.Exeptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public class HttpPlansService : IPlanService
    {
        public readonly HttpClient _httpClient;

        public HttpPlansService(HttpClient httpClient)
        {

            _httpClient = httpClient;
            
        }
        public async Task<ApiResponse<PagedList<PlanSummary>>> GetPlansAsync(string query = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync("/api/v2/Plans");
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<PlanSummary>>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }

        }
    }
}
