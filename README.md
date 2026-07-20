# dotnet_sqlite_ef_template

## プロジェクト構成

```text
dotnet_sqlite_ef_template/
├─ .config/
│  └─ dotnet-tools.json
├─ Controllers/
│  └─ HomeController.cs
├─ Data/
│  └─ AppDbContext.cs
├─ Migrations/
│  ├─ 20260720201732_InitialCreate.cs
│  ├─ 20260720201732_InitialCreate.Designer.cs
│  └─ AppDbContextModelSnapshot.cs
├─ Models/
│  ├─ ErrorViewModel.cs
│  └─ TodoItem.cs
├─ Properties/
│  └─ launchSettings.json
├─ Views/
│  ├─ Home/
│  ├─ Shared/
│  ├─ _ViewImports.cshtml
│  └─ _ViewStart.cshtml
├─ wwwroot/
│  ├─ css/
│  ├─ js/
│  └─ lib/
├─ app.db
├─ appsettings.Development.json
├─ appsettings.json
├─ dotnet_sqlite_ef_template.csproj
├─ Program.cs
└─ setup_log.md
```

## 主な役割

1. `Program.cs`
	- ASP.NET Core の起動設定
	- MVC と EF Core（SQLite）の DI 登録

2. `Data/AppDbContext.cs`
	- EF Core の `DbContext`
	- `TodoItems` テーブル（`DbSet<TodoItem>`）を管理

3. `Models/TodoItem.cs`
	- Todo データのエンティティ

4. `Migrations/`
	- スキーマ変更履歴
	- `dotnet ef migrations add` で更新

5. `appsettings.json`
	- 接続文字列（`ConnectionStrings:DefaultConnection`）などの設定

## 実行手順

### 1. 初回セットアップ

```powershell
# EF CLI ローカルツールの準備（未作成の場合のみ）
dotnet new tool-manifest

# dotnet-ef をローカルインストール
dotnet tool install dotnet-ef --version 9.*
```

既にツールマニフェストがある場合は、次のコマンドで復元できます。

```powershell
dotnet tool restore
```

### 2. アプリ起動

```powershell
dotnet run
```

### 3. マイグレーション追加

```powershell
dotnet ef migrations add <MigrationName>
```

例:

```powershell
dotnet ef migrations add InitialCreate
```

### 4. DB更新（マイグレーション適用）

```powershell
dotnet ef database update
```

### 4.1 app.db の作成手順

`app.db` は手動作成不要で、`dotnet ef database update` 実行時に自動作成されます。

```powershell
# 1) マイグレーション作成（未作成の場合）
dotnet ef migrations add InitialCreate

# 2) DB作成 + マイグレーション適用
dotnet ef database update
```

作成確認:

```powershell
Get-ChildItem .\app.db
```

### 5. よく使う確認コマンド

```powershell
# dotnet-ef が利用可能か確認
dotnet ef --version

# マイグレーション一覧
dotnet ef migrations list
```