﻿using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TeamLegend.Common;
using TeamLegend.Models;
using TeamLegend.Services.Contracts;
using TeamLegend.Web.Areas.Administration.Models.Players;

namespace TeamLegend.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class PlayersController : Controller
    {
        private readonly ILogger<PlayersController> logger;
        private readonly IPlayersService playersService;
        private readonly IMapper mapper;
        private readonly ICloudinaryService cloudinaryService;

        public PlayersController(ILogger<PlayersController> logger, IPlayersService playersService, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            this.logger = logger;
            this.playersService = playersService;
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Player player;
            try
            {
                player = this.mapper.Map<Player>(model);

                var file = model.PlayerPicture;
                if (file != null)
                {
                    var playerPictureId = string.Format(GlobalConstants.PlayerPicture, model.Name);
                    var fileStream = file.OpenReadStream();

                    var imageUploadResult = this.cloudinaryService.UploadPlayerPicture(playerPictureId, fileStream);
                    player.PlayerPictureVersion = imageUploadResult.Version;
                }
                await this.playersService.CreateAsync(player);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Details", "Players", new { area = "", id = player.Id });
        }
    }
}
