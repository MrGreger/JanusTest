﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JanusTest.Janus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JanusTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RoomsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateRoom()
        {
            var client = new JanusClient(new System.Net.Http.HttpClient(), new JanusOptions
            {
                JanusApiUrl = "http://localhost:8088/janus",
                AdminKey = "supersecret",
                AdminSecret = "janusoverlord"
            });

            var room = await client.CreateRoomAsync(Guid.NewGuid().ToString(), "new description");

            return Ok(room);
        }
    }
}

