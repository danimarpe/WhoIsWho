using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhoIsWho.Models;using System.Diagnostics;

namespace WhoIsWho.Controllers
{
    public class AssignationController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: People
        public ActionResult Index()
        {
            int idGame = (int)this.Session["idGame"];
            int idAssigner = (int)this.Session["idAssigner"];

            Assignation assignation = new Assignation { AssignerID = idAssigner, GameID = idGame };
            List<Assignation> allAssignations = db.Assignations.Where((a => a.GameID == idGame)).ToList();

            if (this.isGameStarted())
            {
                assignation = allAssignations.Where(a => a.AssignerID == idAssigner).First();
                if (assignation.Character == null)
                    return View("SetCharacter", assignation);
            }

            if (allAssignations.Where(a => a.AssignerID == idAssigner).Count() == 0)
            {
                db.Assignations.Add(assignation);
                db.SaveChanges();

                this.Assign(idGame);
            }

            return View(db.Assignations.Where((a => a.GameID == idGame && a.AssignedID != idAssigner)).ToList());
        }

        public ActionResult Go(int? idAssigner)
        {
            if (idAssigner != null)
            {
                this.Session["idAssigner"] = idAssigner;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public PartialViewResult GetAssignations()
        {
            if (this.Session["idGame"] == null)
                return null;
            int idGame = (int)this.Session["idGame"];
            int idAssigner = (int)this.Session["idAssigner"];

            return PartialView(db.Assignations.Where((a => a.GameID == idGame && a.AssignedID != idAssigner)).ToList());
        }

        [HttpPost]
        public Boolean TryToStart()
        {
            int idGame = (int)this.Session["idGame"];
            int idAssigner = (int)this.Session["idAssigner"];

            Game currentGame = db.Games.Where(g => g.ID == idGame).First();
            currentGame.PlayersReady = currentGame.PlayersReady + 1;
            db.SaveChanges();

            List<Assignation> allAssignations = db.Assignations.Where((a => a.GameID == idGame)).ToList();
            int playersReady = db.Games.Where(g => g.ID == idGame).First().PlayersReady;
            
            if (playersReady == allAssignations.Count())
            {
                Game game = db.Games.Where(g => g.ID == idGame).ToList().First();
                game.Started = true;
                db.SaveChanges();
            }

            return true;
        }

        private void Assign(int idGame)
        {
            List<Assignation> assignations = db.Assignations.Where((a => a.GameID == idGame)).OrderBy(a => Guid.NewGuid()).ToList();
            int index = 0;
            while (index < assignations.Count())
            {
                assignations[index].Order = index;
                if (index + 1 >= assignations.Count())
                {
                    assignations[index].AssignedID = assignations[0].AssignerID;
                    break;
                }
                int assignedId = assignations[index + 1].AssignerID;
                assignations[index].AssignedID = assignedId;
                index++;
            }

            db.SaveChanges();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetCharacter(string character)
        {
            int idGame = (int)this.Session["idGame"];
            int idAssigner = (int)this.Session["idAssigner"];

            Assignation assignation = db.Assignations.Where((a => a.GameID == idGame && a.AssignerID == idAssigner)).ToList().First();
            assignation.Character = character;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public Boolean isGameStarted()
        {
            int idGame = (int)this.Session["idGame"];
            Game game = db.Games.Where((g => g.ID == idGame)).ToList().First();
            return game.Started;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
