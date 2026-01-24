using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using Microsoft.Maui.ApplicationModel;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HimenoBMT;

namespace HimenoFlex;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object? sender, EventArgs e)
	{
		//「規模」欄に反映
		ram.Text = size.SelectedIndex switch
		{
			-1 => "規模が選択されていません。", // 未選択時
			0 => "SSmall",
			1 => "Small",
			2 => "Middle",
			3 => "Large",
			4 => "ELarge",
			_ => "Size Error",
		};

		if (size.SelectedIndex < 0 || size.SelectedIndex > 4) return;

		int benchmarkSize = size.SelectedIndex switch
		{
			0 => 32,
			1 => 64,
			2 => 128,
			3 => 256,
			4 => 512,
			_ => 0,
		};

		// ベンチマーク前にデータを初期化
		mflops1.Text = "測定中";
		loop.Text = "測定中";
		gosa.Text = "測定中";
		cpu.Text = "測定中";
		pentium.Text = "測定中";

		//ベンチマーク開始
		try
		{
			//ボタンクリックを無効化し、main関数が同時に複数回読み取れないようにする
			Bench_Button.IsEnabled = false;
			Bench_Button.Text = "ベンチマーク中";

			// [TODO] 後で非同期処理にする
			string[] benchmarkResult = HimenoBMT.HimenoBMT.Run(benchmarkSize);

			mflops1.Text = benchmarkResult[0];
			loop.Text = benchmarkResult[0];
			gosa.Text = benchmarkResult[0];
			cpu.Text = benchmarkResult[0];
			pentium.Text = benchmarkResult[0];
		}
		catch (Exception ex)
		{
			ram.Text = $"DLL Error: {ex}";
            mflops1.Text = "";
            loop.Text = "";
            gosa.Text = "";
            cpu.Text = "";
            pentium.Text = "";
        }
		finally
		{
			//初期状態に戻す
			Bench_Button.IsEnabled = true;
			Bench_Button.Text = "ベンチマーク開始";
		}
	}

	private async void Button_Clicked_1(object sender, EventArgs e)
	{
		await SaveFile(CancellationToken.None);

		// ファイル保存(.html)
		async Task SaveFile(CancellationToken cancellationToken)
		{
			using var writer = new MemoryStream();
			
			writer.Write(Encoding.UTF8.GetBytes("<html>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<head>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<title>Himeno Flex ベンチマーク結果</title>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<link rel=\"icon\" href=\"https://lemon73.gitlab.io/favicon.png\">\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<meta charset=\"utf-8\">\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<meta name=\"viewport\" content=\"width=640\">\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<link rel=\"stylesheet\" href=\"https://lemon73.gitlab.io/style.css\">\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("</head>\r\n"));

			writer.Write(Encoding.UTF8.GetBytes("<body>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<header><b><p style=\"padding-left: 8%;\">Himeno Flex</p></b></header>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<main>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<h1>Himeno Flex ベンチマーク結果</h1>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"<p>規模: {ram.Text}</p>"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"<p>MFLOPS: {mflops1.Text}</p>"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"<p>実行ループ回数: {loop.Text}</p>"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"<p>CPU: {cpu.Text}</p>"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"<p>Pentium3 600MHzと比較: {pentium.Text}</p>"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<br />\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("<br />\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"<p>記録日時: {DateTimeOffset.Now:yyyy/MM/dd/HH:mm:ss}</p>"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("</main>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("</body>\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("</html>\r\n"));
			writer.Seek(0, SeekOrigin.Begin);//Androidに出力するときに、保存したファイル内に何も書き込まれない現象の対策

			var fileSaverResult = await FileSaver.Default.SaveAsync($@"HimemoFlex_{DateTimeOffset.Now:yyyyMMdd_HHmmss}.html", writer, cancellationToken);
			/* ファイル保存通知(成功orエラー+原因)
			if (fileSaverResult.IsSuccessful)
			{
				await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show(cancellationToken);
			}
			else
			{
				await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show(cancellationToken);
			}
			*/
		}
	}

	private async void Button_Clicked_2(object sender, EventArgs e)
	{
		await SaveFile(CancellationToken.None);

		// ファイル保存(.txt)
		async Task SaveFile(CancellationToken cancellationToken)
		{
			using var writer = new MemoryStream();

			writer.Write(Encoding.UTF8.GetBytes("Himeno Flex ベンチマーク結果\r\n"));//\r\nは改行コード
			writer.Write(Encoding.UTF8.GetBytes("\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"規模: {ram.Text}"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"MFLOPS: {mflops1.Text}"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"実行ループ回数: {loop.Text}"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"CPU: {cpu.Text}"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"Pentium3 600MHzと比較: {pentium.Text}"+"\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("\r\n"));
			writer.Write(Encoding.UTF8.GetBytes("\r\n"));
			writer.Write(Encoding.UTF8.GetBytes($@"記録日時: {DateTimeOffset.Now:yyyy/MM/dd/HH:mm:ss}"+"\r\n"));
			writer.Seek(0, SeekOrigin.Begin);//Androidに出力するときに、保存したファイル内に何も書き込まれない現象の対策

			var fileSaverResult = await FileSaver.Default.SaveAsync($@"HimemoFlex_{DateTimeOffset.Now:yyyyMMdd_HHmmss}.txt", writer, cancellationToken);
		}
	}
}
