using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CLDV6211POE.Models;

//just add the namespace to the layout to add a new role in future
namespace CLDV6211POE.Controllers
{
    //this code comes from a user on reddit, that i just tweaked to suit my needs
    [Authorize(Roles="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<UserViewModel>();
            var allRoles = _roleManager.Roles.Select(r=>r.Name).ToList();

            foreach(var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                    AllRoles = allRoles,
                    SelectedRole = await _userManager.GetRolesAsync(user).ContinueWith(s=>s.Result.FirstOrDefault())
                };

                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string userId, string selectedRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in currentRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            // Add the user to the new selected role
            await _userManager.AddToRoleAsync(user, selectedRole);

            await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("UserList");
        }
    }
}
