using CRUDelicious.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDelicious.Controllers;

public class DishController : Controller
{
    private CrudContext db;
    public DishController(CrudContext context)
    {
        db = context;
    }

    [HttpGet("/dishes/new")]
    public IActionResult New()
    {
        return View("New");
    }

    [HttpPost("/dishes/create")]
    public IActionResult Create(Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            return New();
        }
        db.Dishes.Add(newDish);
        db.SaveChanges();

        return Redirect("/");
    }

    [HttpGet("")]
    public IActionResult All()
    {
        List<Dish> allDishes = db.Dishes.ToList();

        return View("Index", allDishes);
    }

    [HttpGet("/dishes/{oneDishId}")]
    public IActionResult GetOneDish(int oneDishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == oneDishId); 

        if (dish == null)
        {
            return RedirectToAction("Index");
        }

        return View("OneDish", dish);
    }

    [HttpPost("/dishes/{deletedDishId}/delete")]
    public IActionResult DeleteDish(int deletedDishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == deletedDishId); 

        if (dish != null)
        {
            db.Dishes.Remove(dish);
            db.SaveChanges();
        }

        return Redirect("/");
    }

    [HttpGet("/dishes/{dishId}/edit")]
    public IActionResult Edit(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId); 

        if(dish == null)
        {
            return Redirect("/");
        }
        return View("Edit", dish);
    }

    [HttpPost("/dishes/{dishId}/update")]
    public IActionResult Update(Dish editedDish, int dishId)
    {
        if (ModelState.IsValid == false)
        {
            return Edit(dishId);
        }

        Dish? dbDish = db.Dishes.FirstOrDefault(d => d.DishId == dishId); 

        if (dbDish == null)
        {
            return Redirect("/");
        }

        dbDish.Chef = editedDish.Chef;
        dbDish.Name = editedDish.Name;
        dbDish.Calories = editedDish.Calories;
        dbDish.Tastiness = editedDish.Tastiness;
        dbDish.Description = editedDish.Description;
        dbDish.UpdatedAt = DateTime.Now;

        db.Dishes.Update(dbDish);
        db.SaveChanges();

        return RedirectToAction("GetOneDish", new { dishId = dbDish.DishId });

    }
}