using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoCertificado.Models
{
    public class AtividadeLink
    {        
        public Atividade atividade { get; set; }
        public TipoAtividade tipoAtividade {get;set;}
        public IEnumerable<Atividade> atividades { get; set; }
        public IEnumerable<TipoAtividade> tipoAtividades { get; set; }
    }

}
