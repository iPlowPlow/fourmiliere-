using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.Fabrique;
using LibAbstraite.GestionObjets;
using LibAbstraite.GestionPersonnage;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace LibMetier.GestionEnvironnements
{
    [XmlRoot("fourmilliere"), Serializable]
    public class Fourmiliere : EnvironnementAbstrait
    {


        public Fourmiliere()
        {
            
        }
        public Fourmiliere(int _dimensionX, int _dimensionY)
        {
            this.DimensionX = _dimensionX;
            this.DimensionY = _dimensionY;
       
        }
      
        public override void AjouteChemin(FabriqueAbstraite fan, params AccesAbstrait[] accesArray)
        {
            throw new NotImplementedException();
        }

        public override void AjouteFourmis(PersonnageAbstrait unPersonnage)
        {
            this.PersonnageAbstraitList.Add(unPersonnage);
        }

        public override void AjouteNourriture(ObjetAbstrait unObject)
        {
            this.ObjetAbstraitList.Add(unObject);
        }

        public override void AjouteOeuf(ObjetAbstrait unObject)
        {
            this.ObjetAbstraitList.Add(unObject);
        }

        public override void AjoutePheromone(ObjetAbstrait unObject)
        {
            throw new NotImplementedException();
        }

        public override void AjouteTermite(PersonnageAbstrait unPersonnage)
        {
            this.PersonnageAbstraitList.Add(unPersonnage);
        }

        public override void AjouteZone(params ZoneAbstrait[] zoneArray)
        {
            throw new NotImplementedException();
        }

        public override void ChargerEnv(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }

        public override void ChargerObjet(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }

        public override void ChargerPersonnage(FabriqueAbstraite fab)
        {
            throw new NotImplementedException();
        }

        public override void DeplacerPersonnage(PersonnageAbstrait unPersonnage, ZoneAbstrait zdebut, ZoneAbstrait zfin)
        {
            throw new NotImplementedException();
        }

        public override void Repositioner()
        {
            foreach(var boutDeTerrain in ZoneAbstraitList)
            {
                boutDeTerrain.PersonnageList.AddRange(PersonnageAbstraitList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                boutDeTerrain.ObjetList.AddRange(ObjetAbstraitList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
            }
        }

        public override string Simuler()
        {
            throw new NotImplementedException();
        }

        public override string Statistiques()
        {
            throw new NotImplementedException();
        }
   
    }
}
