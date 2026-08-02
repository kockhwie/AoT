namespace AOT.Services;

// stable id — renaming a display label or re-linking a chapter to a different arc
// never touches chapter data, and the enum itself is language-agnostic. Display text
// lives in the localization resx files via MangaData.ArcNameKeys, same *Key pattern
// CharacterModel/TitanModel already use.
public enum MangaArc
{
    FallOfShiganshina,
    BattleOfTrost,
    TrainingCorps104,
    FemaleTitan,
    ClashOfTitans,
    RoyalGovernment,
    ReturnToShiganshina,
    Marley,
    WarForParadis,
    Rumbling,
    BattleOfHeavenAndEarth
}

public sealed class ChapterModel
{
    public string ChapterLabel { get; init; } = "";
    public MangaArc Arc { get; init; }
    public int[] AnimeEpisodes { get; init; } = Array.Empty<int>();
    public List<string> KeyMoments { get; init; } = new();
    public double? Rating { get; init; }
    public List<string> Tags { get; init; } = new();
}

public sealed class MangaVolumeModel
{
    public int VolumeNumber { get; init; }
    public string CoverImage { get; init; } = "";
    public List<ChapterModel> Chapters { get; init; } = new();

    // every distinct arc this volume's chapters touch, in the order they first appear —
    // a volume shows up on EVERY arc shelf its chapters belong to, not just one "primary".
    public List<MangaArc> Arcs => Chapters.Select(c => c.Arc).Distinct().ToList();

    // ponytail: falls back to averaging whatever chapter ratings exist when the volume
    // itself has none set — covers the "no important moments this volume" mockup case.
    public double DisplayRating => Chapters.Any(c => c.Rating.HasValue)
        ? Chapters.Where(c => c.Rating.HasValue).Average(c => c.Rating!.Value)
        : 0;

    public IEnumerable<string> AllTags => Chapters.SelectMany(c => c.Tags).Distinct();
}

public static class MangaData
{
    // SAMPLE DATA — mock content only, matches the original listing table structure
    public static readonly List<MangaVolumeModel> Volumes = new()
    {
        new MangaVolumeModel
        {
            VolumeNumber = 1, CoverImage = "vol-01.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#1 致兩千年後的你", Arc = MangaArc.FallOfShiganshina, AnimeEpisodes = new[] { 1 },
                    KeyMoments = new() { "超大型巨人、鎧之巨人破牆", "艾連母親被殺" }, Rating = 5, Tags = new() { "超大型巨人", "鎧之巨人", "艾連", "三笠" } },
                new ChapterModel { ChapterLabel = "#2 那一天", Arc = MangaArc.FallOfShiganshina, AnimeEpisodes = new[] { 1, 2 },
                    KeyMoments = new() { "漢尼斯救走艾連與三笠", "難民營與古利夏失蹤" }, Rating = 4, Tags = new() { "艾連", "阿爾敏", "古利夏" } },
                new ChapterModel { ChapterLabel = "#3 夜間的微光", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 3 },
                    KeyMoments = new() { "104期訓練兵團開訓", "艾連克服立體機動裝置平衡練習" }, Rating = 4, Tags = new() { "104期", "基斯" } },
                new ChapterModel { ChapterLabel = "#4 初陣", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 4, 5 },
                    KeyMoments = new() { "超大型巨人睽違五年再度現身", "托洛斯特區城牆被破" }, Rating = 5, Tags = new() { "超大型巨人", "托洛斯特區" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 2, CoverImage = "vol-02.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#5 自尊", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 5 },
                    KeyMoments = new() { "34班慘遭滅隊", "艾連為了救阿爾敏被巨人吞食" }, Rating = 5, Tags = new() { "艾連", "阿爾敏", "初陣" } },
                new ChapterModel { ChapterLabel = "#6 少女看見了世界", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 6 },
                    KeyMoments = new() { "三笠的兒時回憶", "艾連與三笠初次相遇並擊殺強盜" }, Rating = 5, Tags = new() { "三笠", "艾連" } },
                new ChapterModel { ChapterLabel = "#7 小小的刀刃", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 7 },
                    KeyMoments = new() { "三笠氣體耗盡絕望", "神祕神智不清的「神祕巨人」現身暴打其他巨人" }, Rating = 5, Tags = new() { "三笠", "神祕巨人" } },
                new ChapterModel { ChapterLabel = "#8 我可以聽見心跳", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 8 },
                    KeyMoments = new() { "補給站奪回戰", "神祕巨人力盡倒下，體內出現艾連" }, Rating = 5, Tags = new() { "進擊的巨人", "104期" } },
                new ChapterModel { ChapterLabel = "#9 左手的去向", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 9 },
                    KeyMoments = new() { "駐紮兵團包圍艾連", "艾連半巨人化擋下大砲砲彈" }, Rating = 4, Tags = new() { "艾連", "駐紮兵團" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 3, CoverImage = "vol-03.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#10 回應", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 10 },
                    KeyMoments = new() { "阿爾敏發表著名的「人類的價值」演說", "皮克西斯司令出面解圍" }, Rating = 4, Tags = new() { "阿爾敏", "皮克西斯" } },
                new ChapterModel { ChapterLabel = "#11 偶像", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 11 },
                    KeyMoments = new() { "巨石奪回作戰開始", "艾連巨人化後失控攻擊三笠" }, Rating = 4, Tags = new() { "托洛斯特區", "作戰開始" } },
                new ChapterModel { ChapterLabel = "#12 傷痕", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 12 },
                    KeyMoments = new() { "約翰與同伴在街區死守引誘巨人", "阿爾敏用刀刺入巨人後頸喚醒艾連" }, Rating = 4, Tags = new() { "約翰", "阿爾敏" } },
                new ChapterModel { ChapterLabel = "#13 魔彈", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 13, 14 },
                    KeyMoments = new() { "艾連搬運巨石成功堵住牆壁破洞", "里維兵長與調查兵團首度登場救援" }, Rating = 5, Tags = new() { "里維", "調查兵團", "勝利" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 4, CoverImage = "vol-04.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#14 原始的慾望", Arc = MangaArc.BattleOfTrost, AnimeEpisodes = new[] { 14 },
                    KeyMoments = new() { "審議會法庭大戰", "里維兵長當眾暴揍艾連以爭取監護權" }, Rating = 5, Tags = new() { "里維", "審議會", "埃爾文" } },
                new ChapterModel { ChapterLabel = "#15 個別行動", Arc = MangaArc.TrainingCorps104, AnimeEpisodes = new[] { 15 },
                    KeyMoments = new() { "艾連加入特別作戰班（里維班）", "韓吉介紹抓到的實驗巨人「索尼」與「賓」" }, Rating = 4, Tags = new() { "里維班", "韓吉" } },
                new ChapterModel { ChapterLabel = "#16 必要的事", Arc = MangaArc.TrainingCorps104, AnimeEpisodes = new[] { 16 },
                    KeyMoments = new() { "實驗巨人被暗殺", "104期畢業生進行兵團選拔（約翰等人加入調查兵團）" }, Rating = 4, Tags = new() { "104期", "選拔" } },
                new ChapterModel { ChapterLabel = "#17 困惑", Arc = MangaArc.TrainingCorps104, AnimeEpisodes = new[] { 17 },
                    KeyMoments = new() { "第 57 次牆外調查開始", "埃爾文團長擺出長距離搜索陣形" }, Rating = 4, Tags = new() { "牆外調查", "埃爾文" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 5, CoverImage = "vol-05.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#18 森林的出口", Arc = MangaArc.TrainingCorps104, AnimeEpisodes = new[] { 17, 18 },
                    KeyMoments = new() { "阿爾敏遭遇擁有智慧的「女巨人」", "阿爾敏、約翰、萊納合力夾擊女巨人" }, Rating = 5, Tags = new() { "女巨人", "阿爾敏", "萊納" } },
                new ChapterModel { ChapterLabel = "#19 咬", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 19 },
                    KeyMoments = new() { "調查兵團進入巨樹之森", "里維班死守不讓艾連變身，信任同伴" }, Rating = 5, Tags = new() { "里維班", "巨樹之森" } },
                new ChapterModel { ChapterLabel = "#20 史密斯分隊長", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 20 },
                    KeyMoments = new() { "埃爾文設下陷阱用束縛彈成功捕獲女巨人" }, Rating = 5, Tags = new() { "埃爾文", "捕獲作戰" } },
                new ChapterModel { ChapterLabel = "#21 第 57 次牆外調查 1", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 21 },
                    KeyMoments = new() { "女巨人尖叫引來無腦巨人吃掉肉身脫逃", "神祕黑衣人突襲斬殺君達" }, Rating = 5, Tags = new() { "女巨人", "里維班" } },
                new ChapterModel { ChapterLabel = "#22 第 57 次牆外調查 2", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 21, 22 },
                    KeyMoments = new() { "里維班（佩托拉、歐魯、艾魯多）慘遭女巨人全滅", "憤怒的艾連巨人化大戰女巨人" }, Rating = 5, Tags = new() { "里維班", "艾連", "女巨人" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 6, CoverImage = "vol-06.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#23 長毛的巨人", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 23 }, KeyMoments = new() { "阿尼與阿爾敏談話", "阿尼被指認為女巨人" }, Rating = 5, Tags = new() { "阿尼", "阿爾敏", "女巨人" } },
                new ChapterModel { ChapterLabel = "#24 厄特加爾城", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 24 }, KeyMoments = new() { "席安哈特區追捕女巨人", "阿尼巨人化" }, Rating = 5, Tags = new() { "女巨人", "阿尼" } },
                new ChapterModel { ChapterLabel = "#25 士兵", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 24, 25 }, KeyMoments = new() { "艾連在城牆內大戰女巨人" }, Rating = 5, Tags = new() { "艾連", "女巨人" } },
                new ChapterModel { ChapterLabel = "#26 戰士", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 25 }, KeyMoments = new() { "女巨人企圖翻牆逃跑", "米卡莎斬斷其手指", "阿尼結晶化" }, Rating = 5, Tags = new() { "阿尼", "米卡莎", "結晶化" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 7, CoverImage = "vol-07.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#27 士兵們起舞", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 25 }, KeyMoments = new() { "發現牆壁內埋藏著巨人" }, Rating = 5, Tags = new() { "牆中巨人", "尼克神父" } },
                new ChapterModel { ChapterLabel = "#28 東北方", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 26 }, KeyMoments = new() { "羅塞之牆遭突破的警報響起" }, Rating = 4, Tags = new() { "野獸巨人", "米克" } },
                new ChapterModel { ChapterLabel = "#29 戰士們", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 26 }, KeyMoments = new() { "米克隊長遭遇野獸巨人不幸犧牲" }, Rating = 4, Tags = new() { "野獸巨人", "米克" } },
                new ChapterModel { ChapterLabel = "#30 阻礙", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 27 }, KeyMoments = new() { "薩莎回老家救人", "柯尼回到荒廢的拉加哥村" }, Rating = 4, Tags = new() { "薩莎", "柯尼" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 8, CoverImage = "vol-08.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#31 戰士的舞蹈", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 28 }, KeyMoments = new() { "104期南班集結", "發現沒有牆壁破洞" }, Rating = 4, Tags = new() { "調查兵團" } },
                new ChapterModel { ChapterLabel = "#32 大夥兒", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 29 }, KeyMoments = new() { "眾人夜宿厄特加爾城遭巨人包圍" }, Rating = 4, Tags = new() { "厄特加爾城" } },
                new ChapterModel { ChapterLabel = "#33 飛躍", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 29, 30 }, KeyMoments = new() { "前輩組壯烈犧牲", "尤米爾隱藏的身分揭曉" }, Rating = 5, Tags = new() { "尤米爾" } },
                new ChapterModel { ChapterLabel = "#34 毀滅之日", Arc = MangaArc.FemaleTitan, AnimeEpisodes = new[] { 30 }, KeyMoments = new() { "尤米爾巨人化奮戰保衛古堡" }, Rating = 5, Tags = new() { "顎型巨人", "尤米爾" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 9, CoverImage = "vol-09.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#35 野獸巨人", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 30 }, KeyMoments = new() { "調查兵團主力抵達古堡救援" }, Rating = 4, Tags = new() { "里維", "韓吉" } },
                new ChapterModel { ChapterLabel = "#36 漫步", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 31 }, KeyMoments = new() { "漢尼斯帶來羅塞之牆安全的情報" }, Rating = 4, Tags = new() { "漢尼斯" } },
                new ChapterModel { ChapterLabel = "#37 南方", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 31 }, KeyMoments = new() { "調查兵團登上城牆休整" }, Rating = 4, Tags = new() { "城牆" } },
                new ChapterModel { ChapterLabel = "#38 灼熱", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 31 }, KeyMoments = new() { "萊納精神狀態崩潰" }, Rating = 5, Tags = new() { "萊納", "貝特霍爾德" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 10, CoverImage = "vol-10.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#39 士兵", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 31 }, KeyMoments = new() { "世紀名場面：萊納對艾連坦白身分" }, Rating = 5, Tags = new() { "萊納你坐啊", "鎧之巨人", "超大型巨人" } },
                new ChapterModel { ChapterLabel = "#40 尤彌爾", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 32 }, KeyMoments = new() { "艾連怒吼變身與鎧之巨人交戰" }, Rating = 5, Tags = new() { "艾連", "鎧之巨人" } },
                new ChapterModel { ChapterLabel = "#41 歷史", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 32, 33 }, KeyMoments = new() { "超大型巨人壓跨艾連與調查兵團" }, Rating = 5, Tags = new() { "超大型巨人" } },
                new ChapterModel { ChapterLabel = "#42 戰士之名", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 33 }, KeyMoments = new() { "艾連與尤米爾被奪走", "三笠醒來痛哭" }, Rating = 5, Tags = new() { "三笠", "阿爾敏" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 11, CoverImage = "vol-11.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#43 鎧之巨人", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 33 }, KeyMoments = new() { "埃爾文團長率領憲兵團與調查兵團聯軍追擊" }, Rating = 5, Tags = new() { "埃爾文" } },
                new ChapterModel { ChapterLabel = "#44 打擊", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 34 }, KeyMoments = new() { "巨樹之森對峙", "萊納與艾連爭論" }, Rating = 4, Tags = new() { "巨樹之森", "萊納" } },
                new ChapterModel { ChapterLabel = "#45 追趕", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 35 }, KeyMoments = new() { "尤米爾為了克里斯塔反叛聯軍" }, Rating = 4, Tags = new() { "尤米爾", "希斯特莉亞" } },
                new ChapterModel { ChapterLabel = "#46 開場", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 36 }, KeyMoments = new() { "聯軍正面遭遇巨人海", "埃爾文團長手臂被咬斷依舊下令前進" }, Rating = 5, Tags = new() { "埃爾文", "前進" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 12, CoverImage = "vol-12.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#47 孩子", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 36 }, KeyMoments = new() { "阿爾敏用計欺騙貝特霍爾德動搖其心智" }, Rating = 5, Tags = new() { "阿爾敏" } },
                new ChapterModel { ChapterLabel = "#48 大人", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 37 }, KeyMoments = new() { "漢尼斯為了保護艾連與三笠戰死" }, Rating = 5, Tags = new() { "漢尼斯", "吃媽巨" } },
                new ChapterModel { ChapterLabel = "#49 突擊", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 37 }, KeyMoments = new() { "艾連與三笠的雪中告白", "艾連一拳發動始祖座標之力" }, Rating = 5, Tags = new() { "座標", "始祖巨人", "艾連", "三笠" } },
                new ChapterModel { ChapterLabel = "#50 吶喊", Arc = MangaArc.ClashOfTitans, AnimeEpisodes = new[] { 37 }, KeyMoments = new() { "巨人倒戈圍攻萊納等人", "聯軍成功撤退" }, Rating = 5, Tags = new() { "撤退" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 13, CoverImage = "vol-13.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#51 艾連‧耶格爾", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 38 }, KeyMoments = new() { "新里維班成立", "調查兵團被中央政府盯上" }, Rating = 4, Tags = new() { "里維班" } },
                new ChapterModel { ChapterLabel = "#52 溫泉", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 38 }, KeyMoments = new() { "克里斯塔身分曝光：王室私生女希斯特莉亞" }, Rating = 4, Tags = new() { "希斯特莉亞" } },
                new ChapterModel { ChapterLabel = "#53 狼煙", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 38 }, KeyMoments = new() { "艾連硬質化實驗失敗", "中央憲兵綁架計畫" }, Rating = 4, Tags = new() { "硬質化" } },
                new ChapterModel { ChapterLabel = "#54 反擊的信號", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 38, 39 }, KeyMoments = new() { "割喉者凱尼登場", "里維與凱尼遭遇戰" }, Rating = 5, Tags = new() { "凱尼", "里維" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 14, CoverImage = "vol-14.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#55 痛哭", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 39 }, KeyMoments = new() { "里維班在托洛斯特區屋頂進行追逐戰" }, Rating = 4, Tags = new() { "對人立體機動" } },
                new ChapterModel { ChapterLabel = "#56 王座", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 40 }, KeyMoments = new() { "埃爾文與皮克西斯密謀推翻偽王" }, Rating = 4, Tags = new() { "埃爾文", "政治" } },
                new ChapterModel { ChapterLabel = "#57 不可饒恕之人", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 40 }, KeyMoments = new() { "阿爾敏被迫首次開槍擊殺人類" }, Rating = 4, Tags = new() { "阿爾敏" } },
                new ChapterModel { ChapterLabel = "#58 槍聲", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 41 }, KeyMoments = new() { "埃爾文團長被捕", "調查兵團被定罪流放" }, Rating = 4, Tags = new() { "中央憲兵" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 15, CoverImage = "vol-15.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#59 白夜", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 41 }, KeyMoments = new() { "韓吉說服報社揭露真相" }, Rating = 4, Tags = new() { "韓吉", "新聞真相" } },
                new ChapterModel { ChapterLabel = "#60 信賴", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 42 }, KeyMoments = new() { "審判埃爾文", "達里斯總理發動軍事政變成功" }, Rating = 5, Tags = new() { "政變", "達里斯" } },
                new ChapterModel { ChapterLabel = "#61 答覆", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 42, 43 }, KeyMoments = new() { "揭曉真正的王室為雷斯家族", "艾連被鎖在地下教堂" }, Rating = 4, Tags = new() { "雷斯家族" } },
                new ChapterModel { ChapterLabel = "#62 罪", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 43, 44 }, KeyMoments = new() { "艾連看見父親古利夏吃掉芙莉妲奪取始祖的真相" }, Rating = 5, Tags = new() { "古利夏", "始祖巨人" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 16, CoverImage = "vol-16.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#63 願望", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 44 }, KeyMoments = new() { "里維班攻入地下教堂與對人憲兵大戰" }, Rating = 5, Tags = new() { "教堂之戰" } },
                new ChapterModel { ChapterLabel = "#64 歡迎來到奧爾福德區", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 44 }, KeyMoments = new() { "希斯特莉亞拒絕繼承巨人並砸碎針筒" }, Rating = 5, Tags = new() { "女王覺醒", "希斯特莉亞" } },
                new ChapterModel { ChapterLabel = "#65 夢想與詛咒", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 45 }, KeyMoments = new() { "羅德·雷斯喝下藥水化身超巨大畸形巨人", "艾連喝下鎧之藥水獲得硬質化" }, Rating = 5, Tags = new() { "硬質化成功" } },
                new ChapterModel { ChapterLabel = "#66 願望", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 45, 46 }, KeyMoments = new() { "調查兵團全力阻止羅德巨人接近奧爾福德區" }, Rating = 4, Tags = new() { "羅德巨人" } }
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 17, CoverImage = "vol-17.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#67 羅德‧雷斯", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 46, 47 },
                    KeyMoments = new() { "羅德雷斯變成超巨型奇行種", "調查兵團制定牆邊迎擊計畫" }, Rating = 4, Tags = new() { "羅德", "希絲特莉亞" } },
                new ChapterModel { ChapterLabel = "#68 阻擊者", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 47 },
                    KeyMoments = new() { "希絲特莉亞親手斬殺父親羅德", "希絲特莉亞向民眾宣告自己是真正的國王" }, Rating = 5, Tags = new() { "希絲特莉亞", "女王登基" } },
                new ChapterModel { ChapterLabel = "#69 友人", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 48 },
                    KeyMoments = new() { "肯尼臨死前將巨人針劑交給里維", "揭曉阿卡曼家族與烏利王的故事" }, Rating = 5, Tags = new() { "肯尼", "里維", "阿卡曼" } },
                new ChapterModel { ChapterLabel = "#70 我們的新裝備", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 48, 49 },
                    KeyMoments = new() { "希絲特莉亞加冕為王", "調查兵團成功研發新武器雷槍", "獸之巨人擊敗鎧之巨人" }, Rating = 4, Tags = new() { "雷槍", "吉克", "獸之巨人" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 18, CoverImage = "vol-18.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#71 旁觀者", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 49 },
                    KeyMoments = new() { "尋找前教官基斯", "揭曉古利夏當年來到牆內的真相與基斯的過去" }, Rating = 4, Tags = new() { "基斯", "古利夏" } },
                new ChapterModel { ChapterLabel = "#72 夜幕降臨", Arc = MangaArc.RoyalGovernment, AnimeEpisodes = new[] { 50 },
                    KeyMoments = new() { "出征前夜的吃肉狂歡", "艾連、三笠與阿爾敏暢談看海的夢想" }, Rating = 5, Tags = new() { "出征前夜", "看海的夢想" } },
                new ChapterModel { ChapterLabel = "#73 始發之街", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 50, 51 },
                    KeyMoments = new() { "調查兵團重返希干希娜區", "艾連用硬化能力成功封鎖外側城牆破洞" }, Rating = 4, Tags = new() { "希干希娜區", "硬化能力" } },
                new ChapterModel { ChapterLabel = "#74 作戰計畫", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 51, 52 },
                    KeyMoments = new() { "阿爾敏發現城牆內的隱藏空腔", "萊納突襲刺殺兵團士兵後巨人化", "獸之巨人帶領無腦巨人包圍後路" }, Rating = 5, Tags = new() { "萊納", "獸之巨人", "包圍網" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 19, CoverImage = "vol-19.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#75 兩個戰局", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 52 },
                    KeyMoments = new() { "艾連與鎧之巨人展開正面激戰", "埃爾文團長死守內側戰線" }, Rating = 4, Tags = new() { "艾連", "鎧之巨人" } },
                new ChapterModel { ChapterLabel = "#76 雷槍", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 53 },
                    KeyMoments = new() { "調查兵團首度對萊納使用新武器雷槍", "雷槍成功炸碎鎧之巨人的後頸" }, Rating = 5, Tags = new() { "雷槍", "擊碎鎧巨" } },
                new ChapterModel { ChapterLabel = "#77 世界怎麼了", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 53, 54 },
                    KeyMoments = new() { "貝爾托特被獸之巨人投擲進戰場", "回憶馬可臨死前發現萊納與貝爾托特身分的真相" }, Rating = 5, Tags = new() { "馬可之死", "貝爾托特" } },
                new ChapterModel { ChapterLabel = "#78 光景", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 54 },
                    KeyMoments = new() { "貝爾托特在高空變身超大型巨人", "巨大的爆炸熱能將希干希娜區化為火海" }, Rating = 5, Tags = new() { "超大型巨人", "大爆炸" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 20, CoverImage = "vol-20.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#79 完美的比賽", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 55 },
                    KeyMoments = new() { "獸之巨人展開投石碎彈攻勢", "外側調查兵團新兵與馬匹慘遭毀滅性打擊" }, Rating = 4, Tags = new() { "獸之巨人", "絕境" } },
                new ChapterModel { ChapterLabel = "#80 無名之卒", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 55 },
                    KeyMoments = new() { "埃爾文團長發表著名的死亡衝鋒演說", "帶領全體新兵對獸之巨人展開自殺式誘敵衝鋒" }, Rating = 5, Tags = new() { "埃爾文", "團長衝鋒", "名場面" } },
                new ChapterModel { ChapterLabel = "#81 光影", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 56 },
                    KeyMoments = new() { "里維兵長藉由煙幕奇襲獸之巨人", "名場面：兵長砍猴，將吉克肉身從巨人後頸拉出" }, Rating = 5, Tags = new() { "里維", "兵長砍猴", "神作" } },
                new ChapterModel { ChapterLabel = "#82 勇者", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 56 },
                    KeyMoments = new() { "阿爾敏以自身為誘餌被超大型巨人的蒸汽燒焦", "艾連趁隙繞背斬下貝爾托特" }, Rating = 5, Tags = new() { "阿爾敏犧牲", "擊敗超巨" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 21, CoverImage = "vol-21.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#83 大火", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 57 },
                    KeyMoments = new() { "吉克被車力巨人救走", "里維兵長面臨要將針劑救埃爾文還是阿爾敏的重大抉擇" }, Rating = 4, Tags = new() { "車力巨人", "抉擇" } },
                new ChapterModel { ChapterLabel = "#84 白夜", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 57 },
                    KeyMoments = new() { "名場面：白夜的抉擇", "里維決定讓埃爾文安詳離世，阿爾敏注射針劑吃下貝爾托特成為新超巨" }, Rating = 5, Tags = new() { "白夜", "埃爾文之死", "新超巨" } },
                new ChapterModel { ChapterLabel = "#85 地下室", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 58 },
                    KeyMoments = new() { "艾連等人終於打開地下室的大門", "發現古利夏留下的照片，揭曉牆外人類文明依舊繁榮的真相" }, Rating = 5, Tags = new() { "地下室", "世界真相" } },
                new ChapterModel { ChapterLabel = "#86 那一天", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 58, 59 },
                    KeyMoments = new() { "古利夏的童年回憶", "妹妹菲伊慘遭馬雷軍官餵狗殺害", "揭曉艾爾迪亞人與馬雷帝國的歷史恩怨" }, Rating = 5, Tags = new() { "古利夏過去", "馬雷帝國" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 22, CoverImage = "vol-22.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#87 邊境", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 59 },
                    KeyMoments = new() { "古利夏復權派失利被送往樂園化為無腦巨人", "戴娜化為吃掉艾連母親的巨人", "梟現身秒殺馬雷軍艦" }, Rating = 5, Tags = new() { "樂園", "戴娜", "梟" } },
                new ChapterModel { ChapterLabel = "#88 進擊的巨人", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 59 },
                    KeyMoments = new() { "艾連·克魯格向古利夏揭曉這份巨人的名字", "名場面：無論在任何時代，這隻巨人為了追求自由不斷前進，其名為——進擊的巨人" }, Rating = 5, Tags = new() { "進擊的巨人", "自由" } },
                new ChapterModel { ChapterLabel = "#89 會議", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 60 },
                    KeyMoments = new() { "艾連在會議中隱瞞了『觸碰王血無腦巨人能發動始祖能力』的推論以保護希絲特莉亞" }, Rating = 4, Tags = new() { "王血", "始祖謎團" } },
                new ChapterModel { ChapterLabel = "#90 迎向牆壁的彼端", Arc = MangaArc.ReturnToShiganshina, AnimeEpisodes = new[] { 60 },
                    KeyMoments = new() { "牆內巨人被清除乾淨，調查兵團走出牆外來到海邊", "名場面：艾連指著大海彼端問：『把海那邊的敵人全殺光，我們就能獲得自由了嗎？』" }, Rating = 5, Tags = new() { "看海", "終局預告" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 23, CoverImage = "vol-23.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#91 島上的惡魔", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 61 },
                    KeyMoments = new() { "四年後世界，視角切換至馬雷帝國與中東聯合軍的戰爭", "賈碧、法爾可等馬雷戰士候補生首度登場" }, Rating = 4, Tags = new() { "馬雷視角", "賈碧", "法爾可" } },
                new ChapterModel { ChapterLabel = "#92 中東聯合軍", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 61, 62 },
                    KeyMoments = new() { "馬雷軍隊從高空投放無腦巨人進行空襲", "萊納與吉克雙巨降臨攻陷要塞" }, Rating = 4, Tags = new() { "空襲", "斯拉巴要塞" } },
                new ChapterModel { ChapterLabel = "#93 深夜列車", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 62 },
                    KeyMoments = new() { "戰後列車上的休整", "吉克向軍方提議重新啟動帕拉迪島始祖奪還計畫", "法爾可遇見斷腿的傷兵『克魯格』" }, Rating = 4, Tags = new() { "吉克", "傷兵艾連" } },
                new ChapterModel { ChapterLabel = "#94 夜晚的黑暗", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 62, 63 },
                    KeyMoments = new() { "戰士隊回到家鄉雷貝里歐收容區", "萊納與家人聚餐時用微妙的言詞描述島上的惡魔（其實是在想念同伴）" }, Rating = 4, Tags = new() { "雷貝里歐", "萊納" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 24, CoverImage = "vol-24.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#95 騙子", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 63 },
                    KeyMoments = new() { "戰士隊開會商討戴巴家族即將舉辦的祭典", "回憶當年萊納、貝爾托特、亞妮、馬賽破牆前的過去" }, Rating = 4, Tags = new() { "回憶篇", "戴巴家族" } },
                new ChapterModel { ChapterLabel = "#96 希望之門", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 64 },
                    KeyMoments = new() { "馬賽被吃後萊納強行威脅亞妮與貝爾托特繼續前進", "揭曉第一話超大型巨人破牆時背後的戰士視角詳細過程" }, Rating = 5, Tags = new() { "破牆真相", "萊納崩潰" } },
                new ChapterModel { ChapterLabel = "#97 手掌", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 64, 65 },
                    KeyMoments = new() { "法爾可替斷腿傷兵（艾連）寄出祕密信件", "萊納因罪惡感與精神分裂痛苦到意圖含槍自殺，但為了看法爾可而作罷" }, Rating = 5, Tags = new() { "含槍自殺", "傷兵艾連" } },
                new ChapterModel { ChapterLabel = "#98 好前途", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 65, 66 },
                    KeyMoments = new() { "雷貝里歐祭典開幕", "前夜法爾可將萊納單獨帶到一處地下室，名場面：萊納與黑髮艾連時隔四年再度相見" }, Rating = 5, Tags = new() { "命運重逢", "地下室" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 25, CoverImage = "vol-25.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#99 愧疚的陰影", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 66 },
                    KeyMoments = new() { "地下室對談中艾連說出：『我和你一樣』", "舞台上戴巴家族的威利向全世界揭曉牆內卡爾方王不戰之約的真實歷史" }, Rating = 5, Tags = new() { "地下室對談", "不戰之約" } },
                new ChapterModel { ChapterLabel = "#100 宣戰佈告", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 66, 67 },
                    KeyMoments = new() { "威利·戴巴向帕拉迪島正式宣戰", "名場面：『萊納，你坐啊』，隨後艾連在地下室巨人化衝破舞台，吃掉威利" }, Rating = 5, Tags = new() { "萊納你坐啊", "宣戰佈告", "神級高潮" } },
                new ChapterModel { ChapterLabel = "#101 戰鎚巨人", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 67, 68 },
                    KeyMoments = new() { "威利的妹妹變身為戰鎚巨人暴打艾連", "米卡莎與穿著全新黑色對人立體機動裝置的調查兵團從天而降支援" }, Rating = 5, Tags = new() { "戰鎚巨人", "調查兵團登場" } },
                new ChapterModel { ChapterLabel = "#102 突襲", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 68 },
                    KeyMoments = new() { "雷貝里歐收容區淪為戰場", "里維兵長現身奇襲砍翻獸之巨人（演戲演給馬雷看）" }, Rating = 5, Tags = new() { "收容區大戰", "兵長戰獸巨" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 26, CoverImage = "vol-26.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#103 強奪", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 68, 69 },
                    KeyMoments = new() { "阿爾敏化身超大型巨人摧毀馬雷軍港", "顎之巨人遭到調查兵團圍攻重創" }, Rating = 5, Tags = new() { "阿爾敏", "顎之巨人" } },
                new ChapterModel { ChapterLabel = "#104 勝者", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 69 },
                    KeyMoments = new() { "名場面：艾連利用顎之巨人的嘴巴當胡桃鉗咬碎結晶，吞食戰鎚巨人", "飛船前來接應調查兵團撤退" }, Rating = 5, Tags = new() { "吞食戰鎚", "胡桃鉗" } },
                new ChapterModel { ChapterLabel = "#105 兇彈", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 70 },
                    KeyMoments = new() { "賈碧與法爾可強行登上帝國飛船", "賈碧開槍射殺莎夏，全體兵團陷入痛哭" }, Rating = 5, Tags = new() { "莎夏之死", "賈碧" } },
                new ChapterModel { ChapterLabel = "#106 義勇兵", Arc = MangaArc.Marley, AnimeEpisodes = new[] { 71 },
                    KeyMoments = new() { "回憶三年內帕拉迪島的建設", "伊蓮娜帶領的反馬雷義勇兵與島方建交真相" }, Rating = 4, Tags = new() { "義勇兵", "伊蓮娜" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 27, CoverImage = "vol-27.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#107 來訪者", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 72 },
                    KeyMoments = new() { "日出國的清美造訪帕拉迪島", "揭曉吉克提出的『安樂死計畫』與希絲特莉亞懷孕的驚人消息" }, Rating = 4, Tags = new() { "安樂死計畫", "希絲特莉亞" } },
                new ChapterModel { ChapterLabel = "#108 正當防衛", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 72 },
                    KeyMoments = new() { "104期同伴在夕陽火車上互訴真心", "艾連被兵團軟禁，島內局勢開始動盪" }, Rating = 4, Tags = new() { "火車對談", "同伴羈絆" } },
                new ChapterModel { ChapterLabel = "#109 導火線", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 73 },
                    KeyMoments = new() { "賈碧與法爾可越獄逃往帕拉迪島鄉間", "被莎夏的父母好心收留（馬利亞一家的款待）" }, Rating = 4, Tags = new() { "布勞斯一家", "賈碧" } },
                new ChapterModel { ChapterLabel = "#110 偽造", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 74 },
                    KeyMoments = new() { "戴爾多總理遭遇炸彈暗殺身亡", "艾連逃出監獄，『葉卡派』正式宣布成立與中央對立" }, Rating = 4, Tags = new() { "總理之死", "葉卡派" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 28, CoverImage = "vol-28.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#111 兒童們", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 75 },
                    KeyMoments = new() { "尼柯羅在餐廳揭曉紅酒中含有吉克脊髓液的真相", "莎夏的父親選擇原諒賈碧，放下仇恨的鎖鏈" }, Rating = 5, Tags = new() { "脊髓液紅酒", "走出森林" } },
                new ChapterModel { ChapterLabel = "#112 無知", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 75, 76 },
                    KeyMoments = new() { "艾連對三笠與阿爾敏說出極其傷人的狠話：『我最討厭你了』", "阿爾敏憤怒對艾連動手" }, Rating = 5, Tags = new() { "三人決裂", "痛徹心扉" } },
                new ChapterModel { ChapterLabel = "#113 殘酷", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 76 },
                    KeyMoments = new() { "吉克發動咆哮，里維班全體部下化為無腦巨人", "名場面：里維兵長再次重創並活捉獸之巨人" }, Rating = 5, Tags = new() { "里維班巨化", "兵長三砍猴" } },
                new ChapterModel { ChapterLabel = "#114 唯一的救贖", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 77 },
                    KeyMoments = new() { "詳細揭曉吉克的童年回憶與庫薩瓦先生的父子情", "吉克堅信安樂死是艾爾迪亞人唯一的救贖" }, Rating = 5, Tags = new() { "吉克回憶", "庫薩瓦" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 29, CoverImage = "vol-29.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#115 支離破碎", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 78, 79 },
                    KeyMoments = new() { "吉克引爆雷槍，里維兵長被炸重傷瀕死", "神祕的小女孩在道路中用沙土重塑吉克的肉身" }, Rating = 5, Tags = new() { "雷槍引爆", "瀕死里維" } },
                new ChapterModel { ChapterLabel = "#116 天地", Arc = MangaArc.WarForParadis, AnimeEpisodes = new[] { 79 },
                    KeyMoments = new() { "皮克潛入希干希娜區並用槍指向艾連", "馬雷飛艇大軍大舉壓境，天地之戰前哨戰爆發" }, Rating = 4, Tags = new() { "皮克奇襲", "馬雷反擊" } },
                new ChapterModel { ChapterLabel = "#117 審判", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 80 },
                    KeyMoments = new() { "艾連獨自一人正面迎戰鎧之巨人與顎之巨人的夾擊" }, Rating = 4, Tags = new() { "大戰爆發", "艾連孤軍奮戰" } },
                new ChapterModel { ChapterLabel = "#118 突擊", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 80, 81 },
                    KeyMoments = new() { "島方與葉卡派被迫與馬雷軍展開混戰", "吉克登上城牆準備發動咆哮" }, Rating = 4, Tags = new() { "混戰", "吉克登場" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 30, CoverImage = "vol-30.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#119 哥哥與弟弟", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 81 },
                    KeyMoments = new() { "吉克咆哮，法爾可與多名高層化為無腦巨人", "名場面：艾連奔向吉克時，被賈碧用反巨人步槍當場狙擊斷頭" }, Rating = 5, Tags = new() { "吉克咆哮", "艾連斷頭", "神級反轉" } },
                new ChapterModel { ChapterLabel = "#120 道路", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 82 },
                    KeyMoments = new() { "吉克在『道路』中成功接住艾連的頭部", "兩人在道路中相見，吉克發現艾連並未被洗腦" }, Rating = 5, Tags = new() { "座標道路", "兄弟會面" } },
                new ChapterModel { ChapterLabel = "#121 來自未來的你", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 82, 83 },
                    KeyMoments = new() { "名場面：兄弟漫遊古利夏的記憶", "揭曉進擊的巨人能看見未來繼承者記憶的能力", "艾連在過去跨越時空操控古利夏奪取始祖" }, Rating = 5, Tags = new() { "記憶漫遊", "時空悖論", "進擊能力" } },
                new ChapterModel { ChapterLabel = "#122 致兩千年前的你", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 83 },
                    KeyMoments = new() { "揭曉始祖尤彌爾兩千年前悲慘的奴隸過去", "艾連擁抱尤彌爾並賦予她自由，地鳴正式解封，三道城牆崩塌" }, Rating = 5, Tags = new() { "始祖尤彌爾", "地鳴發動", "世紀神作" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 31, CoverImage = "vol-31.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#123 島上的惡魔", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 84, 87 },
                    KeyMoments = new() { "回憶104期首次前往牆外世界的快樂時光", "艾連透過座標向全體艾爾迪亞人宣告：『我要驅逐這世界上所有的生命』" }, Rating = 5, Tags = new() { "滅世宣言", "回憶篇" } },
                new ChapterModel { ChapterLabel = "#124 冰釋", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 84 },
                    KeyMoments = new() { "城牆內的無腦巨人失去控制四處襲擊", "亞妮的結晶化隨之解除，從沉睡中甦醒" }, Rating = 4, Tags = new() { "亞妮解封", "牆內混亂" } },
                new ChapterModel { ChapterLabel = "#125 黃昏", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 85 },
                    KeyMoments = new() { "阿爾敏精神瀕臨崩潰", "亞妮與 hitch 重逢並決定尋找父親" }, Rating = 4, Tags = new() { "亞妮", "阿爾敏" } },
                new ChapterModel { ChapterLabel = "#126 驕傲", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 86 },
                    KeyMoments = new() { "韓吉與重傷的里維與馬雷殘部會合", "『救世小隊』跨越陣營正式成軍：『去拯救世界吧』" }, Rating = 4, Tags = new() { "聯軍成立", "拯救世界" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 32, CoverImage = "vol-32.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#127 終夜", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 88 },
                    KeyMoments = new() { "聯軍在篝火旁爆發激烈的歷史衝突與算帳", "馬可之死的真相被揭開，眾人流淚達成和解" }, Rating = 5, Tags = new() { "篝火夜談", "放下仇恨" } },
                new ChapterModel { ChapterLabel = "#128 叛徒", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 89 },
                    KeyMoments = new() { "港口奪還戰爆發", "阿爾敏與約翰不得不對昔日的同伴葉卡派（弗洛克等人）拔刀相向" }, Rating = 4, Tags = new() { "港口大戰", "昔日同伴" } },
                new ChapterModel { ChapterLabel = "#129 懷舊之情", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 90 },
                    KeyMoments = new() { "基斯教官與馬加特隊長聯手殿後，炸毀軍艦英勇犧牲", "聯軍成功搭乘飛行船逃離" }, Rating = 5, Tags = new() { "大叔犧牲", "飛行船起飛" } },
                new ChapterModel { ChapterLabel = "#130 人類的曙光", Arc = MangaArc.Rumbling, AnimeEpisodes = new[] { 90 },
                    KeyMoments = new() { "超大型巨人軍團橫渡大海抵達馬雷大陸", "馬雷聯合防線被地鳴瞬間摧毀，絕望蔓延世界" }, Rating = 5, Tags = new() { "地鳴登陸", "世界末日" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 33, CoverImage = "vol-33.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#131 地鳴", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 91 }, // 完結篇前篇
                    KeyMoments = new() { "艾連在道路中流淚向難民男孩拉姆齊道歉", "經典震撼分鏡：艾連張開雙臂享受『這就是自由』，而下方是地鳴踐踏慘狀" }, Rating = 5, Tags = new() { "自由的代價", "悲劇高潮", "神分鏡" } },
                new ChapterModel { ChapterLabel = "#132 自由之翼", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 91 },
                    KeyMoments = new() { "弗洛克臨死前開槍破壞飛機油箱", "漢吉團長將職位傳給阿爾敏，獨自一人迎戰地鳴被火焰吞噬犧牲" }, Rating = 5, Tags = new() { "漢吉之死", "自由之翼", "淚崩" } },
                new ChapterModel { ChapterLabel = "#133 罪人們", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 91 },
                    KeyMoments = new() { "艾連透過座標將聯軍拉入道路：『不用談判，想阻止我就來殺了我』", "法爾可覺醒飛翔顎巨能力" }, Rating = 4, Tags = new() { "道路對話", "飛翔顎巨" } },
                new ChapterModel { ChapterLabel = "#134 絕望的深淵", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 91 },
                    KeyMoments = new() { "難民被逼至懸崖邊緣，眾人合力接力保護嬰兒", "聯軍從高空跳傘，對始祖巨人展開最後的空降突擊" }, Rating = 5, Tags = new() { "嬰兒接力", "空降突擊" } },
            }
        },
        new MangaVolumeModel
        {
            VolumeNumber = 34, CoverImage = "vol-34.jpg",
            Chapters = new()
            {
                new ChapterModel { ChapterLabel = "#135 神明與人類", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 92 }, // 完結篇後篇
                    KeyMoments = new() { "始祖尤彌爾召喚歷代九大巨人傀儡軍團進行瘋狂阻擊", "阿爾敏被巨人吞入體內" }, Rating = 5, Tags = new() { "歷代九大巨人", "絕境" } },
                new ChapterModel { ChapterLabel = "#136 獻出心臟吧", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 92 },
                    KeyMoments = new() { "三笠等人拼死掩護，里維兵長下達最後的戰鬥動員令" }, Rating = 4, Tags = new() { "獻出心臟", "最後決戰" } },
                new ChapterModel { ChapterLabel = "#137 巨人", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 92 },
                    KeyMoments = new() { "阿爾敏與吉克在道路中藉由一片樹葉/棒球領悟生命的意義", "歷代英靈（埃爾文、波爾可等）現身倒戈相助，吉克主動現身被里維斬殺，地鳴停止" }, Rating = 5, Tags = new() { "生命的意義", "吉克之死", "地鳴終結" } },
                new ChapterModel { ChapterLabel = "#138 長夢", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 92 },
                    KeyMoments = new() { "怪誕蟲發動最後掙扎，將現場所有艾爾迪亞人化為無腦巨人", "名場面：三笠在木屋長夢幻想中醒來，親手斬下艾連的頭部並與他深情一吻" }, Rating = 5, Tags = new() { "怪誕蟲", "三笠斬首", "最後一吻" } },
                new ChapterModel { ChapterLabel = "#139 走向大樹的丘陵", Arc = MangaArc.BattleOfHeavenAndEarth, AnimeEpisodes = new[] { 92 },
                    KeyMoments = new() { "揭曉艾連私下與同伴在道路告別的全部記憶，巨人力量徹底消失", "三年後世界局勢，三笠將艾連葬在山丘樹下（含漫畫34卷加頁最終結局）" }, Rating = 5, Tags = new() { "悲劇英雄", "大結局", "加頁真相" } },
            }
        },
    };


    public static MangaVolumeModel? FindVolume(int volumeNumber) =>
        Volumes.FirstOrDefault(v => v.VolumeNumber == volumeNumber);

    // canonical arc order (not derived — guarantees the shelf page always shows arcs
    // chronologically even for edge cases like an arc with zero owned volumes).
    public static readonly List<MangaArc> ArcOrder = new()
    {
        MangaArc.FallOfShiganshina,
        MangaArc.BattleOfTrost,
        MangaArc.TrainingCorps104,
        MangaArc.FemaleTitan,
        MangaArc.ClashOfTitans,
        MangaArc.RoyalGovernment,
        MangaArc.ReturnToShiganshina,
        MangaArc.Marley,
        MangaArc.WarForParadis,
        MangaArc.Rumbling,
        MangaArc.BattleOfHeavenAndEarth
    };

    // enum -> resx key, same *Key pattern as CharacterModel.NameKey / TitanModel.NameKey.
    // Resolve with @LocalizationService.GetString(MangaData.ArcNameKeys[arc]) in markup.
    public static readonly Dictionary<MangaArc, string> ArcNameKeys = new()
    {
        { MangaArc.FallOfShiganshina, "Arc_FallOfShiganshina" },
        { MangaArc.BattleOfTrost, "Arc_BattleOfTrost" },
        { MangaArc.TrainingCorps104, "Arc_TrainingCorps104" },
        { MangaArc.FemaleTitan, "Arc_FemaleTitan" },
        { MangaArc.ClashOfTitans, "Arc_ClashOfTitans" },
        { MangaArc.RoyalGovernment, "Arc_RoyalGovernment" },
        { MangaArc.ReturnToShiganshina, "Arc_ReturnToShiganshina" },
        { MangaArc.Marley, "Arc_Marley" },
        { MangaArc.WarForParadis, "Arc_WarForParadis" },
        { MangaArc.Rumbling, "Arc_Rumbling" },
        { MangaArc.BattleOfHeavenAndEarth, "Arc_BattleOfHeavenAndEarth" }
    };

    // volumes belonging to a given arc — a volume appears here for EVERY arc any of
    // its chapters touch, so a boundary volume (e.g. vol.1) shows up on both shelves.
    public static IEnumerable<MangaVolumeModel> VolumesInArc(MangaArc arc) =>
        Volumes.Where(v => v.Arcs.Contains(arc));
}
