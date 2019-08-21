using Fitness.Bisiness.Servises;
using Fitness.DataObject.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Fitness.Web.Controllers
{
    public class ChargesController : Controller
    {
        private readonly IChargeServise _chargeServise;
        private readonly string _connectionString;

        public ChargesController()
        {
            // New 2019-08-20
            _connectionString = @"Data Source=DELL-5490\SQLEXPRESS;Initial Catalog=Fitness;Integrated Security=True";
            //_connectionString = ConnectionStringManager.GetConnectionString("Fitness");
            if (String.IsNullOrEmpty(_connectionString))
            {
                ConnectionStringManager.AddConnectionString("Fitness", @"Data Source=DELL-5490\SQLEXPRESS;Initial Catalog=Fitness;Integrated Security=True");
                _connectionString = ConnectionStringManager.GetConnectionString("Fitness");
            }

            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IChargeServise>().To<ChargeServise>().WithConstructorArgument("stringConnection", _connectionString);
            _chargeServise = ninjectKernel.Get<IChargeServise>();

            // Old 2019-08-19
            //_chargeServise = new ChargeServise(Properties.Settings.Default.DataBase);
        }

        // GET: Charges
        public ActionResult Index()
        {
            var result = _chargeServise.GetList();
            return View(result);
        }

        // GET: Store/Products/Create
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(ChargeExtendet chargeExtendet)
        {
            if (ModelState.IsValid)
            {
                _chargeServise.Insert(chargeExtendet);
                return RedirectToAction("Index");
            }

            return View(chargeExtendet);
        }

        // GET: Store/Products/Edit
        public ActionResult Update(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChargeExtendet chargeExtendet = _chargeServise.GetItem(id.Value);
            if (chargeExtendet == null)
            {
                return HttpNotFound();
            }
            return View(chargeExtendet);
        }

        // POST: Store/Products/Edit
        [HttpPost]
        public ActionResult Update(ChargeExtendet chargeExtendet)
        {
            if (ModelState.IsValid)
            {
                _chargeServise.Update(chargeExtendet);
                return RedirectToAction("Index");
            }
            return View(chargeExtendet);
        }

        // GET: Store/Products/Delete
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _chargeServise.Delete(id.Value);
            return RedirectToAction("Index");
        }
    }
}