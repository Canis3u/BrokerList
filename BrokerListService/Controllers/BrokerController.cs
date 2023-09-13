using AutoMapper;
using BrokerListService.Service.Interface;
using BrokerListService.ServiceModel;
using BrokerListService.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrokerListService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IBrokerListGenrateService _brokerListGenrateService;
        private IBrokerService _brokerService;
        public BrokerController(IMapper mapper, IBrokerListGenrateService brokerListGenrateService, IBrokerService brokerService)
        {
            _mapper = mapper;
            _brokerListGenrateService = brokerListGenrateService;
            _brokerService = brokerService;
        }

        // generate brokerList service
        // POST api/<BrokerListController>
        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            Console.WriteLine(nameof(PostAsync));
            var rowschanges = await _brokerListGenrateService.GetDataFromApiAsync();
            return Ok(rowschanges);
        }

        // delete brokerList service
        // DELETE api/<BrokerListController>/
        [HttpDelete("{deleteCode}")]
        public async Task<IActionResult> DeleteAsync(string deleteCode)
        {
            Console.WriteLine(nameof(DeleteAsync));
            var rowschanges = await _brokerService.DeleteBrokerAsync(deleteCode);
            return Ok(rowschanges);
        }

        // query brokerList service
        // GET: api/<BrokerListController>
        [HttpGet]
        public async Task<IEnumerable<BrokerRespViewModel>> GetBrokerAsync([FromQuery] BrokerQueryViewModel viewModel)
        {
            Console.WriteLine(nameof(GetBrokerAsync));
            var queryServiceModel = _mapper.Map<BrokerQueryServiceModel>(viewModel);
            var respServiceModel = await _brokerService.GetBrokersAsync(queryServiceModel);
            var respViewModel = _mapper.Map< List<BrokerRespViewModel>>(respServiceModel);
            return respViewModel;
        }
    }
}
