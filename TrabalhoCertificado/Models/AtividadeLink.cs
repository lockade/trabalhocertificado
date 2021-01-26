using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoCertificado.Models
{
    public class AtividadeLink
    {       
        //POST
        public Atividade atividade { get; set; }
        public TipoAtividade tipoAtividade {get;set;}
        public int ano { get; set; }
        //GET
        public IEnumerable<Atividade> atividades { get; set; }
        public IEnumerable<TipoAtividade> tipoAtividades { get; set; }
        public IEnumerable<int> anos { get; set; }
    }

}
