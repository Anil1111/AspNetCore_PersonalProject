﻿namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Models.League;
    using TeamLegend.Models;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class LeaguesController : AdministrationController
    {
        private readonly ILogger<LeaguesController> logger;
        private readonly ILeaguesService leaguesService;
        private readonly IMapper mapper;

        public LeaguesController(ILogger<LeaguesController> logger, ILeaguesService leaguesService, IMapper mapper)
        {
            this.logger = logger;
            this.leaguesService = leaguesService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(LeagueCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            League league;
            try
            {
                league = this.mapper.Map<League>(model);
                this.leaguesService.CreateAsync(league);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                return this.View(model);
            }
            
            return this.RedirectToAction("Details", "Leagues", new { area = "", id = league.Id });
        }
    }
}
