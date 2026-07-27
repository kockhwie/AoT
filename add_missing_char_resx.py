# add_missing_char_resx.py
import xml.etree.ElementTree as ET
import os

def add_to_resx(filepath, entries):
    tree = ET.parse(filepath)
    root = tree.getroot()
    existing_keys = set(d.attrib['name'] for d in root.findall('data'))
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
    'CharHistoria_Title': 'Queen of the Walls',
    'CharHistoria_Bio': 'The illegitimate daughter of the Reiss royal bloodline, thrust into the crown after refusing to inherit the Founding Titan. Later ascended the throne to guide Paradis through its darkest hour.',
    'CharYmir_Title': '104th Training Corps / Former Jaw Titan Holder',
    'CharYmir_Bio': 'A mysterious cadet of the 104th Training Corps who once held the Jaw Titan. Fiercely devoted to Historia, she sacrificed herself to protect the ones she loved.',
    'CharSpecialOps_Title': 'Special Operations Squad Member',
    'CharSpecialOps_Bio': "A founding member of Levi's original Special Operations Squad, chosen for exceptional skill with vertical maneuvering equipment and unwavering loyalty to the Scout Corps.",
    'TitanFinal_H': 'Height: Variable',
    'TitanFinal_Desc': 'The evolved form of the Founding Titan after the Rumbling, stripped of its ability to control the Nine Titans\u2019 successors but still bound to Ymir\u2019s curse.',
    'TitanSonnyBean_H': 'Height: 4m ~ 7m',
    'TitanSonnyBean_Desc': "A pair of pure titans captured alive by the Scout Corps for experimentation. Their behavior offered the first scientific insights into titan physiology and regeneration.",
    'TitanWall_H': 'Height: ~50m (Dormant)',
    'TitanWall_Desc': 'Countless titans embedded within the three walls since their construction, waiting to be awakened. Their existence remained hidden for a century until the truth was revealed.',
}
entries_zh = {
    'CharHistoria_Title': '牆內女王',
    'CharHistoria_Bio': '雷斯王室的私生女，拒絕繼承始祖巨人之力後被迫登上王座，肩負起帶領帕拉迪島度過最黑暗時刻的重任。',
    'CharYmir_Title': '104期訓練兵團 / 前顎之巨人持有者',
    'CharYmir_Bio': '104期訓練兵團中身世成謎的學員，曾是顎之巨人的持有者。對希絲特莉亞抱持著深厚的情誼，最終為守護所愛之人犧牲自己。',
    'CharSpecialOps_Title': '特別作戰班成員',
    'CharSpecialOps_Bio': '里維最初特別作戰班的創始成員，因立體機動裝置操作技術出眾及對調查兵團的絕對忠誠而入選。',
    'TitanFinal_H': '身高：不定',
    'TitanFinal_Desc': '地鳴事件後始祖巨人的最終型態，喪失了操控九大巨人繼承者的能力，卻依然受尤彌爾詛咒的束縛。',
    'TitanSonnyBean_H': '身高：4米～7米',
    'TitanSonnyBean_Desc': '被調查兵團活捉用於實驗的一對無垢巨人。牠們的行為模式為人類首次提供了關於巨人生理機能與再生能力的科學觀察數據。',
    'TitanWall_H': '身高：約50米（沉睡狀態）',
    'TitanWall_Desc': '自城牆建造以來便被埋藏於三道城牆之中、等待甦醒的無數巨人。牠們的存在被隱藏了整整一個世紀，直到真相揭曉的那天。',
}
entries_ja = {
    'CharHistoria_Title': '壁の女王',
    'CharHistoria_Bio': 'レイス王家の私生児。始祖の巨人の継承を拒み、やがて玉座に就いてパラディ島を最も暗い時代へと導いた。',
    'CharYmir_Title': '104期訓練兵団 / 元顎の巨人保有者',
    'CharYmir_Bio': '104期訓練兵団に所属する謎めいた訓練生で、かつて顎の巨人を宿していた。ヒストリアへの深い想いを胸に、愛する者を守るため自らを犠牲にした。',
    'CharSpecialOps_Title': '特別作戦班メンバー',
    'CharSpecialOps_Bio': 'リヴァイ班の創設メンバーの一人。卓越した立体機動装置の技術と調査兵団への揺るぎない忠誠心により選抜された。',
    'TitanFinal_H': '身長：不定',
    'TitanFinal_Desc': '地鳴らしの後に変化した始祖の巨人の最終形態。九つの巨人の継承者を操る力を失ったが、ユミルの呪縛には依然として縛られている。',
    'TitanSonnyBean_H': '身長：4m～7m',
    'TitanSonnyBean_Desc': '調査兵団によって生け捕られ、実験に用いられた無垢の巨人の一対。その行動様式は、巨人の生理機能と再生能力に関する初の科学的知見をもたらした。',
    'TitanWall_H': '身長：約50m（休眠状態）',
    'TitanWall_Desc': '城壁が築かれた当初から三重の壁の中に埋め込まれ、覚醒の時を待つ無数の巨人たち。その存在は真実が明かされるまで一世紀にわたり隠され続けた。',
}

base_path = r"c:\Users\User\source\repos\AOT\Resources"
add_to_resx(os.path.join(base_path, "AppStrings.resx"), entries_en)
add_to_resx(os.path.join(base_path, "AppStrings.zh.resx"), entries_zh)
add_to_resx(os.path.join(base_path, "AppStrings.ja.resx"), entries_ja)
print("Added missing character/titan keys.")