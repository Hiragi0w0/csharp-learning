using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

using Class;

public class ItemApiClient
{
	private readonly HttpClient client = new HttpClient
	{
		BaseAddress = new("http://localhost:5037")
	};
	private readonly CancellationToken cancellationToken = new CancellationToken();

	public async Task<List<Item>?> GetAllItems()
	{
		// Jsonで取得
		return await client.GetFromJsonAsync<List<Item>>("/items", cancellationToken);
	}
}