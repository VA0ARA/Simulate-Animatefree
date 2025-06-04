using APAF.AppServices.Domain;
using APAF.Domain.Core.Contracts.AppServices;
using APAF.Domain.Core.Dtos.Owner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AFAP.EndPointMVCPattern.Controllers.Owner
{
    [Authorize(Roles = "Owner ,Admin")]
    public class OwnerController : Controller
    {
        #region Property-Constructore
        private readonly IOwnerAppService _ownerRepositoryAppService;

        public OwnerController(IOwnerAppService ownerRepositoryAppService)
        {
            _ownerRepositoryAppService = ownerRepositoryAppService;
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult CreateOwner()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner(OwnerDto ownerDto, CancellationToken cancellationToken)
        {
            await _ownerRepositoryAppService.Create(ownerDto.FullName, ownerDto.Password, ownerDto.UserName, ownerDto.DoesItRemove, ownerDto.role, cancellationToken);
            return RedirectToAction("CreateOwner");
        }
        #endregion
        #region ShowDetailPanel
        [HttpGet]
        public async Task<IActionResult> OwnerShowDetailPanel(CancellationToken cancellationToken)
        {
            var ListOfOwners = await _ownerRepositoryAppService.GetAll(cancellationToken);
            var ListOfActiveOwner = ListOfOwners.Where(p => p.DoesItRemove == false).ToList();
            return View(ListOfActiveOwner);
        }
        #endregion
        #region Edit
        //[HttpGet]
        //public async Task<IActionResult> EditOnwer(long Id, CancellationToken cancellationToken)
        //{

        //    var AdminDto = await _ownerRepositoryAppService.GetById(Id, cancellationToken);
        //    if (AdminDto != null)
        //    {
        //        return View(AdminDto);
        //    }
        //    else
        //    {
        //        return RedirectToAction("EditAdmin");
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> EditAdmin(AdminDto objDto, CancellationToken cancellationToken)
        //{
        //    if (objDto != null)
        //    {
        //        await _adminRepositoryAppService.Edit(objDto.Id, objDto.FullName, objDto.Password, objDto.UserName, objDto.DoesItRemove, cancellationToken);
        //        return RedirectToAction("GetAllAdmin");
        //    }
        //    else
        //    {
        //        return RedirectToAction("EditAdmin");
        //    }

        //}
        #endregion
        #region Delete
        /*        [HttpPost]
                public async Task<IActionResult> DeleteAdmin(Owner objDto, CancellationToken cancellationToken)
                {
                    if (objDto != null)
                    {
                        await _adminRepositoryAppService.Remove(objDto.Id,cancellationToken);
                        return RedirectToAction("GetAllAdmin");
                    }
                    else
                    {
                        return RedirectToAction("EditAdmin");
                    }

                }*/
        #endregion
    }
}
