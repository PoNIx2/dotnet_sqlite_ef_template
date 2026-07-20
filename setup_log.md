# setup_log

## 概要
`dotnet ef migrations add InitialCreate` / `dotnet ef database update` 実行時に発生したエラーを解消し、SQLite への初回マイグレーション適用まで完了した。

## 発生していた事象
1. `dotnet-ef` が見つからず EF CLI 実行不可
2. EF CLI 導入後、`No DbContext was found in assembly` が発生

## 実施した修正内容
1. EF CLI（ローカルツール）を導入
- `dotnet new tool-manifest`
- `dotnet tool install dotnet-ef --version 9.*`

2. EF Core の実行対象を追加
- `Data/AppDbContext.cs` を作成
  - `AppDbContext : DbContext`
  - `DbSet<TodoItem> TodoItems`
- `Models/TodoItem.cs` を作成
  - `Id`, `Title`, `IsDone` を定義

3. DI 設定と DB 接続設定を追加
- `Program.cs`
  - `AddDbContext<AppDbContext>(...)`
  - `UseSqlite(...)`
- `appsettings.json`
  - `ConnectionStrings:DefaultConnection = Data Source=app.db`

4. マイグレーションを生成
- `Migrations/20260720201732_InitialCreate.cs`
- `Migrations/20260720201732_InitialCreate.Designer.cs`
- `Migrations/AppDbContextModelSnapshot.cs`

## 実行コマンドと結果
1. `dotnet ef migrations add InitialCreate`
- 結果: 成功

2. `dotnet ef database update`
- 結果: 成功
- 補足: `TodoItems` テーブル作成、`__EFMigrationsHistory` 更新を確認

## 確認観点（チェックリスト）
1. ツール
- `dotnet ef --version` が実行可能か
- `.config/dotnet-tools.json` に `dotnet-ef` が登録されているか

2. アプリ構成
- `DbContext` クラスが存在するか
- `Program.cs` に `AddDbContext` 登録があるか
- `appsettings.json` に接続文字列があるか

3. マイグレーション状態
- `Migrations/` 配下に `InitialCreate` 一式が生成されているか
- `dotnet ef migrations list` で対象マイグレーションが表示されるか

4. DB 反映
- `dotnet ef database update` が成功するか
- `app.db` が生成/更新されるか

## 再発時の最短復旧手順
1. `dotnet ef --version` で CLI の有無確認
2. なければ `dotnet tool install dotnet-ef --version 9.*`（ローカル推奨）
3. `DbContext` / `AddDbContext` / 接続文字列の3点を確認
4. `dotnet ef migrations add <MigrationName>`
5. `dotnet ef database update`
