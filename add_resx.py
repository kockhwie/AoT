import xml.etree.ElementTree as ET
import os

def add_to_resx(filepath, entries):
    tree = ET.parse(filepath)
    root = tree.getroot()
    
    # check if key already exists to avoid duplicates
    existing_keys = set([data.attrib['name'] for data in root.findall('data')])
    
    modified = False
    for key, val in entries.items():
        if key not in existing_keys:
            data = ET.Element('data', {'name': key, 'xml:space': 'preserve'})
            value = ET.SubElement(data, 'value')
            value.text = val
            root.append(data)
            modified = True
            
    if modified:
        tree.write(filepath, encoding='utf-8', xml_declaration=True)

entries_en = {
    'Author_PageTitle': '諫山 創 (Hajime Isayama) — Attack on Titan Creator',
    'Author_MetaDesc': 'Biography and details about Hajime Isayama, the creator and mangaka of Attack on Titan (Shingeki no Kyojin).',
    'Author_Badge': 'Mangaka',
    'Author_Title': 'Creator & Illustrator of Attack on Titan',
    'Author_StatBorn': 'Born',
    'Author_StatHometown': 'Hometown',
    'Author_StatPublisher': 'Publisher',
    'Author_StatActiveYears': 'Active Years',
    'Author_ValBorn': 'Aug 29, 1986',
    'Author_ValHometown': 'Ōita, Japan',
    'Author_ValPublisher': 'Kodansha',
    'Author_ValActive': '2006 – Present',
    'Author_SecBio': 'Creative Biography',
    'Author_SecAwards': 'Awards & Honors',
    'Author_SecLegacy': 'Legacy & Influence',
    'Author_Bio1': 'Born in Oyama, Ōita Prefecture, Japan, Hajime Isayama dreamed of creating manga from an early age. His breakthrough came when he moved to Tokyo and submitted a short version of Attack on Titan (Shingeki no Kyojin) to Kodansha. Despite early critiques of his raw art style, the series was serialized in Bessatsu Shōnen Magazine starting in 2009.',
    'Author_Bio2': 'Over the next 12 years, Isayama crafted a narrative masterpiece that redefined modern dark fantasy and shōnen manga. Blending survival horror, political intrigue, and complex moral dilemmas, the story of humanity\'s struggle against the Titans captivated a global audience. The manga concluded in April 2021 after 139 chapters.',
    'Author_Quote': '"I think the story of Attack on Titan is about the struggle to break free from the cage of pre-determined destiny, and the heavy price of freedom."',
    'Author_Award1Title': 'Kodansha Manga Award',
    'Author_Award1Desc': 'Won the 35th annual award in the Shōnen category for Attack on Titan.',
    'Author_Award2Title': 'Micheluzzi Award',
    'Author_Award2Desc': 'Awarded Best Foreign Series at the Napoli COMICON in Italy.',
    'Author_Award3Title': 'Harvey Award Nomination',
    'Author_Award3Desc': 'Nominated for Best American Edition of Foreign Material.',
    'Author_Award4Title': 'Angoulême Festival Special Award',
    'Author_Award4Desc': 'Honored with the Special 50th Anniversary Prize at the prestigious French festival.',
    'Author_MetricVolumes': 'Manga Volumes',
    'Author_MetricChapters': 'Chapters',
    'Author_MetricCopies': 'Copies Sold Worldwide',
    'Author_LegacyText': 'Isayama\'s work transcends standard pop culture. Attack on Titan has inspired major anime adaptations, live-action feature films, light novels, video games, and numerous academic essays exploring its complex themes of cyclical hatred, nationalism, and collective trauma. His storytelling is recognized globally for its intricate foreshadowing and unforgettable narrative payoffs.',
}

entries_zh = {
    'Author_PageTitle': '諫山 創 (Hajime Isayama) — 進擊的巨人作者',
    'Author_MetaDesc': '關於《進擊的巨人》創作者與漫畫家諫山創的生平與詳細介紹。',
    'Author_Badge': '漫畫家',
    'Author_Title': '《進擊的巨人》創作者與插畫家',
    'Author_StatBorn': '出生',
    'Author_StatHometown': '故鄉',
    'Author_StatPublisher': '出版社',
    'Author_StatActiveYears': '活躍年份',
    'Author_ValBorn': '1986年8月29日',
    'Author_ValHometown': '日本大分縣',
    'Author_ValPublisher': '講談社',
    'Author_ValActive': '2006年 – 至今',
    'Author_SecBio': '創作生平',
    'Author_SecAwards': '獎項與榮譽',
    'Author_SecLegacy': '傳奇與影響力',
    'Author_Bio1': '諫山創出生於日本大分縣大山町，從小就夢想著創作漫畫。當他搬到東京並向講談社提交《進擊的巨人》短篇版時，他迎來了突破。儘管早期的粗獷畫風曾受到批評，但該系列仍於2009年開始在《別冊少年Magazine》連載。',
    'Author_Bio2': '在接下來的12年中，諫山創精心打造了一部重新定義現代黑暗奇幻和少年漫畫的敘事傑作。融合了生存恐怖、政治陰謀和複雜的道德困境，人類與巨人抗爭的故事吸引了全球觀眾。該漫畫於2021年4月完結，共139話。',
    'Author_Quote': '「我認為《進擊的巨人》的故事是關於掙脫命中注定的牢籠的鬥爭，以及為自由付出的沉重代價。」',
    'Author_Award1Title': '講談社漫畫賞',
    'Author_Award1Desc': '憑藉《進擊的巨人》榮獲第35屆少年部門大賞。',
    'Author_Award2Title': '米歇魯齊獎',
    'Author_Award2Desc': '在義大利拿坡里國際漫畫節榮獲最佳外國系列獎。',
    'Author_Award3Title': '哈維獎提名',
    'Author_Award3Desc': '獲提名為最佳美國版外國作品。',
    'Author_Award4Title': '安古蘭國際漫畫節特別獎',
    'Author_Award4Desc': '在法國這項久負盛名的漫畫節上榮獲50週年特別獎。',
    'Author_MetricVolumes': '漫畫單行本',
    'Author_MetricChapters': '漫畫話數',
    'Author_MetricCopies': '全球銷量',
    'Author_LegacyText': '諫山創的作品超越了標準的流行文化。《進擊的巨人》啟發了大型動畫改編、真人電影、輕小說、電子遊戲，以及無數探討其循環仇恨、民族主義和集體創傷等複雜主題的學術論文。他的敘事因其錯綜複雜的伏筆和令人難忘的故事收尾而享譽全球。',
}

entries_ja = {
    'Author_PageTitle': '諫山 創 (Hajime Isayama) — 進撃の巨人 作者',
    'Author_MetaDesc': '『進撃の巨人』の作者である漫画家・諫山創の経歴と詳細について。',
    'Author_Badge': '漫画家',
    'Author_Title': '『進撃の巨人』原作者',
    'Author_StatBorn': '生年月日',
    'Author_StatHometown': '出身地',
    'Author_StatPublisher': '出版社',
    'Author_StatActiveYears': '活動期間',
    'Author_ValBorn': '1986年8月29日',
    'Author_ValHometown': '日本・大分県',
    'Author_ValPublisher': '講談社',
    'Author_ValActive': '2006年 – 現在',
    'Author_SecBio': '経歴',
    'Author_SecAwards': '受賞歴',
    'Author_SecLegacy': '影響とレガシー',
    'Author_Bio1': '日本・大分県大山町に生まれた諫山創は、幼い頃から漫画を描くことを夢見ていました。上京し、『進撃の巨人』の読み切り版を講談社に持ち込んだことでブレイクを果たします。当初はその粗削りな画風に批判もありましたが、2009年より『別冊少年マガジン』にて連載が開始されました。',
    'Author_Bio2': 'その後12年間にわたり、諫山創は現代のダークファンタジーと少年漫画の概念を再定義する傑作を紡ぎ出しました。サバイバルホラー、政治的陰謀、そして複雑な道徳的ジレンマを融合させ、巨人と戦う人類の物語は世界中の読者を魅了しました。漫画は2021年4月に全139話で完結しました。',
    'Author_Quote': '「『進撃の巨人』の物語は、あらかじめ決められた運命の檻から抜け出そうとする闘いと、自由のための重い代償について描いていると思います」',
    'Author_Award1Title': '講談社漫画賞',
    'Author_Award1Desc': '『進撃の巨人』で第35回少年部門を受賞。',
    'Author_Award2Title': 'ミケルッツィ賞',
    'Author_Award2Desc': 'イタリアのナポリ・コミコンにて最優秀外国作品賞を受賞。',
    'Author_Award3Title': 'ハーベイ賞 ノミネート',
    'Author_Award3Desc': '最優秀アメリカ版外国作品部門にノミネート。',
    'Author_Award4Title': 'アングレーム国際漫画祭 特別賞',
    'Author_Award4Desc': 'フランスの権威ある漫画祭にて50周年記念特別賞を受賞。',
    'Author_MetricVolumes': '単行本巻数',
    'Author_MetricChapters': '総話数',
    'Author_MetricCopies': '全世界累計発行部数',
    'Author_LegacyText': '諫山創の作品は一般的なポップカルチャーの枠を超えています。『進撃の巨人』は、大規模なアニメーション化、実写映画、ライトノベル、ビデオゲームを生み出し、憎しみの連鎖、ナショナリズム、集団的トラウマといった複雑なテーマを探求する数多くの学術論文の題材ともなっています。彼の物語は、その緻密な伏線と忘れられない結末により、世界中で高く評価されています。',
}

base_path = r"c:\Users\User\source\repos\AOT\Resources"
add_to_resx(os.path.join(base_path, "AppStrings.resx"), entries_en)
add_to_resx(os.path.join(base_path, "AppStrings.zh.resx"), entries_zh)
add_to_resx(os.path.join(base_path, "AppStrings.ja.resx"), entries_ja)

print("Added keys successfully.")
