using AutoMapper;
using IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class OpenIdConnectClientController : Controller
    {
        private readonly IMapper _mapper;

        public OpenIdConnectClientController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OpenIdConnectClientViewModel clientDto)
        {
            if (!ModelState.IsValid)
            {
                return View(clientDto);
            }

            try
            {
                var client = _mapper.Map<IdentityServer4.Models.Client>(clientDto);

                DynamicClientStore.Client.ClientId = client.ClientId;
                DynamicClientStore.Client.IdentityTokenLifetime = client.IdentityTokenLifetime;
                DynamicClientStore.Client.RedirectUris = client.RedirectUris;
                DynamicClientStore.Client.PostLogoutRedirectUris = client.PostLogoutRedirectUris;

                return View();
            }
            catch
            {
                return View(clientDto);
            }
        }

        public ActionResult Edit()
        {
            var clientDto = _mapper.Map<OpenIdConnectClientViewModel>(DynamicClientStore.Client);
            return View(clientDto);
        }

        public ActionResult Reset()
        {
            DynamicClientStore.ResetConfigClient();
            return RedirectToAction("Edit");
        }
    }
}