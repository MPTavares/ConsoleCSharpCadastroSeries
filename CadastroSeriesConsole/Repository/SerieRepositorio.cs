using CadastroSeriesConsole.Entities;
using CadastroSeriesConsole.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CadastroSeriesConsole.Services
{
    class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();
        const string fileName = "series.json";

        public SerieRepositorio()
        {
        }       

        public void Atualizar(int id, Serie entidade)
        {
            var json = File.ReadAllText(Path.GetFullPath(fileName));
            listaSerie = JsonConvert.DeserializeObject<List<Serie>>(json);
            var indexUpdate = listaSerie.FindIndex(s => s.Id == id);
            listaSerie[indexUpdate] = entidade;
            UpdateFileJson(listaSerie);
        }

        public void Excluir(int id)
        {
            var json = File.ReadAllText(Path.GetFullPath(fileName));
            listaSerie = JsonConvert.DeserializeObject<List<Serie>>(json);
            var indexUpdate = listaSerie.FindIndex(s => s.Id == id);
            listaSerie[indexUpdate].Excluido = true;
            UpdateFileJson(listaSerie);
        }

        public void Inserir(Serie entidade)
        {
            List<Serie> listaSerie = new List<Serie>();
            var json = File.ReadAllText(Path.GetFullPath(fileName));
            listaSerie = JsonConvert.DeserializeObject<List<Serie>>(json);         
            listaSerie.Add(entidade);
            UpdateFileJson(listaSerie);
        }

        public List<Serie> Listar()
        {
            List<Serie> items;
            using (StreamReader r = new StreamReader(Path.GetFullPath(fileName)))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Serie>>(json);
            };
            return items;
        }

        public int ProximoId()
        {
            int Id = 0;
            var json = File.ReadAllText(Path.GetFullPath(fileName));
            listaSerie = JsonConvert.DeserializeObject<List<Serie>>(json);
            foreach(Serie serie in listaSerie){
               if (serie.Id > Id)
                {
                    Id = serie.Id;
                }
            }
            return Id + 1;
        }

        public Serie RetornarPorId(int id)
        {
            var json = File.ReadAllText(Path.GetFullPath(fileName));
            listaSerie = JsonConvert.DeserializeObject<List<Serie>>(json);
            var viewObj = listaSerie.FirstOrDefault(s => s.Id == id);       
            return viewObj;
        }
        void UpdateFileJson(List<Serie> listaSerie)
        {
            string novoJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(listaSerie, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(fileName, novoJsonResult);
        }
    }
}
