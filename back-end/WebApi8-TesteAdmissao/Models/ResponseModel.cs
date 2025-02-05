namespace WebApi8_TesteAdmissao.Models
{
    public class ResponseModel<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status {  get; set; } = true;

        public int TotalPaginas {  get; set; } = 1 ;

        public int TotalRegistros { get; set; } = 1;
    }
}
