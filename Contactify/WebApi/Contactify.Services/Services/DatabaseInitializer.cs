﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Contactify.DataLayer.Interfaces;
using Contactify.DataTransferObjects.InputModels;
using Contactify.Entities.Models;
using Contactify.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Contactify.Services.Services
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAccountService accountService;
        private readonly IContactifyData data;

        public DatabaseInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAccountService accountService,
            IContactifyData data)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.accountService = accountService;
            this.data = data;
        }

        public async Task Seed()
        {
            await this.SeedUser();
        }

        private async Task SeedUser()
        {
            if (!this.data.ApplicationUser.Query().Any(u => u.UserName == "admin"))
            {
                var adminModel = new RegisterUserInputModel()
                {
                    Username = "admin",
                    Email = "admin@contactify.io",
                    Password = "123123",
                    Firstname = "Administrator",
                    Lastname = "Administrator"
                };

                await this.accountService.RegisterUser(adminModel);

                var createAdminRole = await this.roleManager.CreateAsync(new IdentityRole("Administrator"));

                if (!createAdminRole.Succeeded)
                {
                    throw new Exception(string.Join("; ", createAdminRole.Errors));
                }

                var adminUser = this.data.ApplicationUser.Query().FirstOrDefault();
                var addAdminRole = await this.userManager.AddToRoleAsync(adminUser, "Administrator");

                if (!addAdminRole.Succeeded)
                {
                    throw new Exception(string.Join("; ", addAdminRole.Errors));
                }

                this.data.SaveChanges();
            }
        }
    }
}