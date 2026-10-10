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

	public async Task<List<Item>?> GetAllItems(CancellationToken cancellationToken = default)
	{
		// Jsonで取得
		return await client.GetFromJsonAsync<List<Item>>("/items", cancellationToken);
	}

	public async Task<Item?> GetItem(int id, CancellationToken cancellationToken = default)
	{
		// Jsonで取得
		return await client.GetFromJsonAsync<Item>($"/items/{id}", cancellationToken);
	}

	public async Task<object?> GetSlow(int ms, CancellationToken cancellationToken = default)
	{
		// Jsonで取得
		return await client.GetFromJsonAsync<object>($"slow?ms={ms}", cancellationToken);
	}

	public async Task<HttpStatusCode> GetError(CancellationToken cancellationToken = default)
	{
		// Jsonで取得
		using var response = await client.GetAsync("/error", cancellationToken);
		return response.StatusCode;
	}
}