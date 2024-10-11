namespace wep_app_domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade_Estoque { get; set; }
        public DateTime Data_Criacao { get; set; }
    }
}
