using System.Runtime.Serialization;

namespace StatisticsWebApp.Models
{
	[DataContract]
	public class DataPoint
	{
		public DataPoint(string color, int altitude, string city)
		{
			this.Color = color;
			this.Altitude = altitude;
			this.City = city;	
		}

		[DataMember(Name = "color")]
		public string Color = "";

		[DataMember(Name = "altitude")]
		public int Altitude = 0;

		[DataMember(Name = "city")]
		public string City = "";
	}
}
