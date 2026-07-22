import xml.etree.ElementTree as ET
import os

def add_to_resx(filepath, entries):
    tree = ET.parse(filepath)
    root = tree.getroot()
    existing_keys = set([d.attrib['name'] for d in root.findall('data')])
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
    'CharGarrison_Title': 'Garrison Regiment Veteran',
    'CharGarrison_Bio': 'Stationed on the walls to defend the interior districts from breaches. Known for a fierce, unwavering loyalty to the people of Shiganshina.',
    'CharMP_Title': 'Military Police Commander',
    'CharMP_Bio': 'Commands the Military Police Brigade tasked with keeping order within the walls, often caught between civic duty and the corruption of the Interior.',
    'CharInterior_Title': 'Chief of the First Interior Squad',
    'CharInterior_Bio': "A ruthless enforcer of the crown's will, wielding absolute authority over the Military Police's most secretive operations.",
    'CharRoyal_Title': 'Member of the Reiss Bloodline',
    'CharRoyal_Bio': 'Carries the blood of the true royal family, hidden from history to protect the secret of the Founding Titan.',
    'CharGov_Title': 'Royal Government Official',
    'CharGov_Bio': "Serves within the walls' ruling administration, navigating the political machinery that has controlled Paradis for a century.",
    'CharCivilian_Title': 'Paradis Island Physician',
    'CharCivilian_Bio': "A former Marleyan Warrior candidate who infiltrated Wall Maria disguised as a doctor, carrying the secret of the Attack and Founding Titans.",
    'CharMarley_Title': 'Marleyan Warrior Unit',
    'CharMarley_Bio': 'Raised from childhood on Paradis-hating propaganda, trained to inherit a Titan power and serve the Marleyan military.',
    'CharTybur_Title': 'Head of the Tybur Family',
    'CharTybur_Bio': "Steward of the War Hammer Titan and the only family permitted to declare war on behalf of the Eldian people.",
    'CharYeagerist_Title': 'Yeagerist Militia Leader',
    'CharYeagerist_Bio': "A former Scout who turned to radical loyalty toward Eren Yeager's cause after Marley's assault on Liberio.",
}
entries_zh = {
    'CharGarrison_Title': '駐紮兵團老兵',
    'CharGarrison_Bio': '駐守城牆以防禦內部區域被突破，以對希干希納區居民堅定不移的忠誠著稱。',
    'CharMP_Title': '憲兵團指揮官',
    'CharMP_Bio': '統領負責維持牆內秩序的憲兵團，時常在公職與中央的腐敗之間掙扎。',
    'CharInterior_Title': '中央第一憲兵團隊長',
    'CharInterior_Bio': '無情執行王室意志的執法者，掌握憲兵團最機密行動的絕對權力。',
    'CharRoyal_Title': '雷斯王室血脈成員',
    'CharRoyal_Bio': '身懷真正的王室血統，為保護始祖巨人的祕密而隱姓埋名於歷史之外。',
    'CharGov_Title': '王政府官員',
    'CharGov_Bio': '任職於牆內統治機構，周旋在控制帕拉迪島百年的政治機器之中。',
    'CharCivilian_Title': '帕拉迪島醫師',
    'CharCivilian_Bio': '曾是馬雷戰士候補生，偽裝成醫生潛入瑪利亞之牆，身懷進擊與始祖巨人的祕密。',
    'CharMarley_Title': '馬雷戰士隊',
    'CharMarley_Bio': '自幼被灌輸仇恨帕拉迪島的思想長大，受訓繼承巨人之力並效忠馬雷軍。',
    'CharTybur_Title': '戴巴家族當家',
    'CharTybur_Bio': '戰槌巨人的守護者，也是唯一被允許代表艾爾迪亞人宣戰的家族。',
    'CharYeagerist_Title': '葉卡派民兵領袖',
    'CharYeagerist_Bio': '曾是調查兵團一員，在馬雷突襲雷貝里歐後轉而狂熱效忠艾連的理念。',
}
entries_ja = {
    'CharGarrison_Title': '駐屯兵団ベテラン',
    'CharGarrison_Bio': '内部区域を壁の破壊から守るために配置。シガンシナ区の人々への揺るぎない忠誠で知られる。',
    'CharMP_Title': '憲兵団司令官',
    'CharMP_Bio': '壁内の秩序維持を担う憲兵団を指揮し、公務と中央の腐敗の間で板挟みになることも多い。',
    'CharInterior_Title': '中央第一憲兵団隊長',
    'CharInterior_Bio': '王の意志を冷徹に執行する者。憲兵団最も機密性の高い作戦に絶対的権限を持つ。',
    'CharRoyal_Title': 'レイス王家の血統',
    'CharRoyal_Bio': '始祖の巨人の秘密を守るため、歴史から隠された真の王家の血を引く。',
    'CharGov_Title': '王政府職員',
    'CharGov_Bio': 'パラディ島を百年支配してきた政治機構の中で、壁内の統治機関に従事する。',
    'CharCivilian_Title': 'パラディ島の医師',
    'CharCivilian_Bio': 'かつてのマーレ戦士候補生。医師を装ってウォール・マリアに潜入し、進撃と始祖の巨人の秘密を持つ。',
    'CharMarley_Title': 'マーレ戦士隊',
    'CharMarley_Bio': '幼少期からパラディ島を憎む教育を受け、巨人の力を継承しマーレ軍に仕えるべく訓練された。',
    'CharTybur_Title': 'タイバー家当主',
    'CharTybur_Bio': '戦槌の巨人の管理者であり、エルディア人を代表して宣戦布告できる唯一の一族。',
    'CharYeagerist_Title': 'イェーガー派民兵指導者',
    'CharYeagerist_Bio': 'かつて調査兵団の一員だったが、レベリオ強襲後エレン・イェーガーの思想に急進的に傾倒した。',
}

base_path = r"c:\Users\User\source\repos\AOT\Resources"
add_to_resx(os.path.join(base_path, "AppStrings.resx"), entries_en)
add_to_resx(os.path.join(base_path, "AppStrings.zh.resx"), entries_zh)
add_to_resx(os.path.join(base_path, "AppStrings.ja.resx"), entries_ja)
print("Added character keys.")