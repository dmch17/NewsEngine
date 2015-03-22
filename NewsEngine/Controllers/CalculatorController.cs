using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsEngine.Models;

namespace NewsEngine.Controllers
{
    public class CalculatorController : Controller
    {
        public ActionResult CalcTestAdd(CalculatorModel calculator)
        {
            return View(calculator);
        }

        [HttpGet]
        public ActionResult CalcForm()
        {
            ViewBag.Operations = GetOperations();
            return View(new CalculatorModel());
        }

        [HttpPost]
        public ActionResult CalcForm(CalculatorModel calculator)
        {
            ViewBag.Operations = GetOperations();
            return View(calculator);
        }

        private List<SelectListItem> GetOperations()
        {
            List<SelectListItem> operations = new List<SelectListItem>();
            operations.Add(new SelectListItem { Value = CalculatorModel.CalculatorOperation.Add.ToString(), Text = CalculatorModel.CalculatorOperation.Add.ToString(), Selected = true });
            operations.Add(new SelectListItem { Value = CalculatorModel.CalculatorOperation.Subtraction.ToString(), Text = CalculatorModel.CalculatorOperation.Subtraction.ToString(), Selected = false });
            operations.Add(new SelectListItem { Value = CalculatorModel.CalculatorOperation.Multiplication.ToString(), Text = CalculatorModel.CalculatorOperation.Multiplication.ToString(), Selected = false });
            operations.Add(new SelectListItem { Value = CalculatorModel.CalculatorOperation.Division.ToString(), Text = CalculatorModel.CalculatorOperation.Division.ToString(), Selected = false });
            return operations;
        }
    }
}