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
        //après le déplacement du personnage, ajoute les 4 zones adjacentes à la zone du personnage dans les zones accessibles du
        //personnage
        public override void AjouteChemin(PersonnageAbstrait unpersonnage)
        {
            unpersonnage.ChoixZoneSuivante.Zonesaccessibles.Clear();
            CoordonneesAbstrait positionPersonnage = unpersonnage.Position;
            for (int i = -1; i<=1; i+=2)
            {
                for(int j = -1; j <= 1; j += 2)
                {
                    try
                    {
                        unpersonnage.ChoixZoneSuivante.Zonesaccessibles.Add(ZoneAbstraitList
                            .Single(z => z.Position.X == positionPersonnage.X + i && z.Position.Y == positionPersonnage.X + j));
                    }
                    catch(Exception e)
                    {

                    }
                }
            }
        }

        public override void AjouteFourmis(PersonnageAbstrait uneFourmi)
        {
            this.PersonnageAbstraitList.Add(uneFourmi);
        }

        public override void AjouteCombattante(PersonnageAbstrait uneCombattante)
        {
            this.PersonnageAbstraitList.Add(uneCombattante);
        }

        public override void AjouteNourriture(ObjetAbstrait nourriture)
        {
            this.ObjetAbstraitList.Add(nourriture);
        }

        public override void AjouteOeuf(ObjetAbstrait unOeuf)
        {
            this.ObjetAbstraitList.Add(unOeuf);
        }

        public override void AjoutePheromone(ObjetAbstrait unPheromone)
        {
            this.ObjetAbstraitList.Add(unPheromone);
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
                boutDeTerrain.PersonnageList.Clear();
                boutDeTerrain.ObjetList.Clear();
                boutDeTerrain.PersonnageList.AddRange(PersonnageAbstraitList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())));
                boutDeTerrain.ObjetList.AddRange(ObjetAbstraitList.Where(x => x.Position.toString().Equals(boutDeTerrain.Position.toString())));

                //boutDeTerrain.OuvriereList.AddRange(PersonnageAbstraitList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                //boutDeTerrain.GuerrierList.AddRange(PersonnageAbstraitList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                //boutDeTerrain.ReineList.AddRange(PersonnageAbstraitList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                //boutDeTerrain.TermiteList.AddRange(PersonnageAbstraitList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
                //boutDeTerrain.ObjetList.AddRange(ObjetAbstraitList.Where(x => x.position.toString().Equals(boutDeTerrain.position.toString())));
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
