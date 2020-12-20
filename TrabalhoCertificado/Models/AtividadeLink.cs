using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoCertificado.Models
{
    public class AtividadeLink
    {
        public List<Atividade> atividades { get; set; }
        public Atividade atividade { get; set; }
        public TipoAtividade tipoAtividade {get;set;}
        public List<TipoAtividade> tipoAtividades { get; set; }
    }
}
