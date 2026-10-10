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

	public async Task<Item?> GetItem(int id)
	{
		// Jsonで取得
		return await client.GetFromJsonAsync<Item>($"/items/{id}", cancellationToken);
	}

	public async Task<object?> GetSlow(int ms)
	{
		// Jsonで取得
		return await client.GetFromJsonAsync<object>($"slow?ms={ms}", cancellationToken);
	}

	public async Task<HttpStatusCode> GetError()
	{
		// Jsonで取得
		using var response = await client.GetAsync("/error", cancellationToken);
		response.EnsureSuccessStatusCode();
		return response.StatusCode;
	}
}