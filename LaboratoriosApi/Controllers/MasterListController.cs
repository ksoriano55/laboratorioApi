using Microsoft.AspNetCore.Mvc;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Laboratorios.Core.ViewModels;
using Newtonsoft.Json;
using Syncfusion.OfficeChart.Implementation;
using Microsoft.OpenApi.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace LaboratoriosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterListController : ControllerBase
    {
        private readonly IMasterListRepository _masterListRepository;

        public MasterListController(IMasterListRepository masterListRepository)
        {
            _masterListRepository = masterListRepository;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetMasterList()
        {
            try
            {
               var masterList = await _masterListRepository.GetMasterList();
               return Ok(masterList);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }            
        }

        [HttpGet, Authorize]
        [Route("getHistory")]
        public async Task<IActionResult> GetHistory()
        {
            try
            {
                var logs = await _masterListRepository.GetLogHistory();
                return Ok(logs);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("insert")]
        public IActionResult upload([FromForm] IFormFile? document, IFormCollection masterDocument, [FromForm] string action, [FromForm] string changeVersion, IFormCollection? documentChanges)
        {
            try
            {
                /* Obtener Id de Usuario*/
                string jwtEncoded = Request.Headers.Authorization;
                jwtEncoded = jwtEncoded.Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jwtDecoded = handler.ReadToken(jwtEncoded) as JwtSecurityToken;
                var userId = jwtDecoded.Claims.FirstOrDefault(j => j.Type.EndsWith("id")).Value;
                var id = Convert.ToInt32(userId);


                var change = changeVersion == "0" ? false : true;
                MasterList json = new MasterList();
                DocumentChangesLog changes = new DocumentChangesLog();
                if (masterDocument.TryGetValue("masterDocument", out var someData))
                {
                    json = JsonConvert.DeserializeObject<MasterList>(someData.First());
                }
                if (documentChanges.TryGetValue("documentChanges", out var someDataChanges))
                {
                    changes = JsonConvert.DeserializeObject<DocumentChangesLog>(someDataChanges.First());
                }
                var result = _masterListRepository.insert(document, json,changes, action, change, id);
                if (result.Status.ToString()=="RanToCompletion")
                {
                    return Ok("Ok");
                }
                var message = result.Exception.InnerException.Message;
                return BadRequest(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
