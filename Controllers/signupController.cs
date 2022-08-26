﻿using Microsoft.AspNetCore.Mvc;
using dMart.Models;
using DMART.Models.Repositories;
using DMART.Models.Interfaces;

namespace dMart.Controllers
{
    public class signupController : Controller
    {
        private readonly IUserRepository usersRepo;

        [HttpGet]
        public ViewResult Signup()
        {
            return View("signup");
        }

        [HttpPost]
        public IActionResult Signup(users user)
        {
            if (ModelState.IsValid)
            {
                if (usersRepo.validateNewUser(user))
                {
                    usersRepo.RegisterUser(user);
                    return RedirectToAction("Login");
                }
                else
                {
                    string msg = "User with this email or phone number already exist. Please login with your existing account";
                    return View("Error", msg);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please enter correct data");
            }
            return View();
        }


        [HttpGet]
        public ViewResult Login()
        {
            return View("login");
        }

        [HttpPost]
        public IActionResult Login(users user)
        {
            if (usersRepo.validateLogin(user))
            {
                return RedirectToAction("Index", "Home");
            }

            string msg = "Wrong login credentials. Please Try again";
            return View("Error", msg);
        }
    }
}