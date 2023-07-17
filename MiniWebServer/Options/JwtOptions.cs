using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniWebServer.Options
{
    public class JwtOptions
    {
        private double _expireMins;

        [Required(AllowEmptyStrings = false)]
        public string? Issuer { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Audience { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Key { get; set; }

        [Range(1.0, double.MaxValue)]
        public double ExprireMins
        {
            get { return _expireMins == 0 ? 5.0 : _expireMins; }
            set { _expireMins = value; }
        }
    }
}
