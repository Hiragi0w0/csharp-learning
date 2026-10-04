var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<EmployeeStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 部署絞り込み
app.MapGet("/employees", (string? department, EmployeeStore store) => 
{
    var resSortedYear = store.GetAll().OrderBy(el => el.iJoinedYear);

	if (department is null) return Results.Ok(resSortedYear);

    var resGrepDepartment = resSortedYear.Where(el => el.strDepartment == department);
	return Results.Ok(resGrepDepartment);
})
.WithName("GetEmployees");

// 社員データ取得
// 引数：iID＝ID
app.MapGet("/employees/{iID}", (int iID, EmployeeStore store) =>
{
    var employee = store.Get(iID);
    if (employee is null) {
        return Results.NotFound();
    }
    return Results.Ok(employee);
});

// 社員データ追加
// 引数：employee＝追加する社員データ
app.MapPost("/employees", (Employee data, EmployeeStore store) =>
{
	if (!store.Add(data))
	{
		return Results.BadRequest(new { message = "社員を登録できませんでした。" });
	}
	return Results.Created($"/employees/{data.iID}", data);
});

// 社員データ削除
// 引数：iID＝削除する社員ID
app.MapDelete("/employees/{iID}", (int iID, EmployeeStore store) =>
{
    if (!store.Delete(iID))
    {
        return Results.NotFound();
    }
    return Results.NoContent();
});


// 社員データ更新
// 引数：employee＝追加する社員データ
app.MapPut("/employees/{iID}", (int iID, Employee data, EmployeeStore store) =>
{
	if (iID != data.iID)
	{
		return Results.BadRequest(new { message = "URLとデータのIDが一致しません。" });
	}

	if (store.Get(iID) is null)
	{
		return Results.NotFound();
	}
	if (!store.Update(data))
	{
		return Results.BadRequest(new { message = "更新するデータが不正です" });
	}
	return Results.NoContent();
});

app.Run();