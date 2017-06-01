using LibAbstraite.Fabrique;
using LibAbstraite.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using System.Xml.Serialization;

namespace LibAbstraite.GestionEnvironnement
{

    public abstract class EnvironnementAbstrait
    {
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public List<AccesAbstrait> AccesAbstraitList { get; set; }
      
        public List<ObjetAbstrait> ObjetAbstraitList { get; set; }

        public List<PersonnageAbstrait> PersonnageAbstraitList { get; set; }
        public List<ZoneAbstrait> ZoneAbstraitList { get; set; }
        public abstract void AjouteOeuf(ObjetAbstrait unObject);
        public abstract void AjouteChemin(PersonnageAbstrait unPersonnage);
        public abstract void AjoutePheromone(ObjetAbstrait unObject);
        public abstract void AjouteNourriture(ObjetAbstrait unObject);
        public abstract void AjouteFourmis(PersonnageAbstrait unPersonnage);
        public abstract void AjouteCombattante(PersonnageAbstrait unPersonnage);
        public abstract void AjouteTermite(PersonnageAbstrait unPersonnage);
        public abstract void AjouteZone(params ZoneAbstrait[] zoneArray);
        public abstract void ChargerEnv(FabriqueAbstraite fab);
        public abstract void ChargerPersonnage(FabriqueAbstraite fab);
        public abstract void ChargerObjet(FabriqueAbstraite fab);
        public abstract void DeplacerPersonnage(PersonnageAbstrait unPersonnage,ZoneAbstrait zdebut, ZoneAbstrait zfin);
        public abstract void Repositioner();
        public abstract string Simuler();
        public abstract string Statistiques();
   

    }
}
