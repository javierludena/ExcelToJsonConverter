using Newtonsoft.Json;
using System;

namespace FrameworkApiExample.Models
{
    /// <summary>
    /// Clase para deserializar los datos del fichero de carga desde Excel
    /// Mapea las columnas con espacios y caracteres especiales usando JsonProperty
    /// </summary>
    public class FicheroCarga
    {
        [JsonProperty("Fecha de Albarán")]
        public double FechaAlbaran { get; set; }

        [JsonProperty("Fecha Contabilización Venta")]
        public double FechaContabilizacionVenta { get; set; }

        [JsonProperty("Organización ventas")]
        public string OrganizacionVentas { get; set; }

        [JsonProperty("Solicitante")]
        public string Solicitante { get; set; }

        [JsonProperty("Nombre Solicitante")]
        public string NombreSolicitante { get; set; }

        [JsonProperty("Material")]
        public string Material { get; set; }

        [JsonProperty("Texto breve de material")]
        public string TextoBreveMaterial { get; set; }

        [JsonProperty("Cantidad de pedido")]
        public double CantidadPedido { get; set; }

        [JsonProperty("Un.medida venta")]
        public string UnidadMedidaVenta { get; set; }

        [JsonProperty("Num. Factura")]
        public string NumeroFactura { get; set; }

        [JsonProperty("Importe")]
        public double Importe { get; set; }

        [JsonProperty("Ingreso")]
        public double Ingreso { get; set; }

        [JsonProperty("Número de Pesada")]
        public string NumeroPesada { get; set; }

        [JsonProperty("Pago Propietario")]
        public double PagoPropietario { get; set; }

        [JsonProperty("Pago Suministrador")]
        public double PagoSuministrador { get; set; }

        [JsonProperty("Importe del Transporte")]
        public double ImporteTransporte { get; set; }

        [JsonProperty("Precio Aprovechamiento")]
        public double PrecioAprovechamiento { get; set; }

        [JsonProperty("Importe Grúa")]
        public double ImporteGrua { get; set; }

        [JsonProperty("Gasto")]
        public double Gasto { get; set; }

        [JsonProperty("Margen")]
        public double Margen { get; set; }

        [JsonProperty("Código Zona")]
        public string CodigoZona { get; set; }

        [JsonProperty("Nº Pedido de Venta")]
        public string NumeroPedidoVenta { get; set; }

        [JsonProperty("Pedido de compra")]
        public string PedidoCompra { get; set; }

        [JsonProperty("Ubicación técnica ¿? De compra o de venta")]
        public string UbicacionTecnica { get; set; }
    }
}