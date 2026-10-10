using Class;

var client = new ItemApiClient();
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

try
{
	// GET /items
	Console.WriteLine("GET /items");
	var items = await client.GetAllItems(cts.Token);
	if (items is not null)
	{
		int size = items.Count();
		for (int i = 0; i < size; i++)
		{
			Console.WriteLine($"{items[i].ToString()}, {items[i].name}, {(items[i].price).ToString()}");
		}
	}

	// GET /items/{id}
	Console.WriteLine("GET /items/{id}");

	int id = 0;
	string? input = "";
	while (!int.TryParse(input, out id))
	{
		Console.WriteLine("Please Input ID: ");
		input = Console.ReadLine();
		if (input is null) return;
	}
	id = int.Parse(input);

	var item = await client.GetItem(id, cts.Token);
	if (item is not null)
	{
		Console.WriteLine($"{item.ToString()}, {item.name}, {(item.price).ToString()}");
	}

	// GET /slow?ms=**
	Console.WriteLine("GET /slow?ms=**");
	int ms = 0;
	input = "";
	while (!int.TryParse(input, out ms))
	{
		Console.WriteLine("Please Input ms: ");
		input = Console.ReadLine();
		if (input is null) return;
	}
	ms = int.Parse(input);

	var resultSlow = await client.GetSlow(ms, cts.Token);
	if (resultSlow is not null)
	{
		Console.WriteLine($"{resultSlow}");
	}


	// GET /error
	Console.WriteLine("GET /error");
	var resultError = await client.GetError(cts.Token);
	Console.WriteLine($"{(int)resultError}");
}
catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
{
	Console.WriteLine($"見つかりません: {(int)ex.StatusCode.Value}");
}
catch (HttpRequestException ex) when ((ex.StatusCode is not null) && ((int)ex.StatusCode.Value >= 500))
{
	Console.WriteLine($"サーバーエラー: {(int)ex.StatusCode.Value}");
}
catch (HttpRequestException ex) when (ex.StatusCode is null)
{
	Console.WriteLine("サーバーに接続できません。");
}
catch (OperationCanceledException) when (cts.IsCancellationRequested)
{
	Console.WriteLine("タイムアウトしました。");
}
catch (Exception ex)
{
	Console.WriteLine($"Error: {ex}");
}