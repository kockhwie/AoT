import xml.etree.ElementTree as ET
import os

def add_to_resx(filepath, entries):
    tree = ET.parse(filepath)
    root = tree.getroot()
    existing_keys = set([d.attrib['name'] for d in root.findall('data')])
    modified = False
    for key, val in entries.items():
        if key in existing_keys:
            # update existing if needed or skip
            for d in root.findall('data'):
                if d.attrib['name'] == key:
                    val_elem = d.find('value')
                    if val_elem is not None and val_elem.text != val:
                        val_elem.text = val
                        modified = True
        else:
            data = ET.Element('data', {'name': key, 'xml:space': 'preserve'})
            value = ET.SubElement(data, 'value')
            value.text = val
            root.append(data)
            modified = True
    if modified:
        tree.write(filepath, encoding='utf-8', xml_declaration=True)

entries_en = {
    'Slide1_Title': 'The Breach',
    'Slide1_Quote': 'On that day, mankind received a grim reminder of the terror of being ruled by them...',
    'Slide2_Title': "Humanity's Strongest",
    'Slide2_Quote': 'A person who cannot sacrifice anything, cannot change anything.',
    'Slide3_Title': 'The Roar of Attack',
    'Slide3_Quote': 'I will exterminate them... every single one of them from this world!',
}

entries_zh = {
    'Slide1_Title': '牆的破壞',
    'Slide1_Quote': '那一天，人類回想起了，受支配的恐怖…',
    'Slide2_Title': '最強的兵士',
    'Slide2_Quote': '什麼都無法捨棄的人，什麼也改變不了。',
    'Slide3_Title': '進擊的咆哮',
    'Slide3_Quote': '我要將牠們驅逐出去，一個不留！',
}

entries_ja = {
    'Slide1_Title': '壁の破壊',
    'Slide1_Quote': 'その日 人類は思い出した ヤツらに支配されていた恐怖を…',
    'Slide2_Title': '最強の兵士',
    'Slide2_Quote': '何も捨てることができない人には、何も変えることはできない。',
    'Slide3_Title': '進撃の咆哮',
    'Slide3_Quote': '駆逐してやる、この世から一匹残らず！',
}

base_path = r"c:\Users\User\source\repos\AOT\Resources"
add_to_resx(os.path.join(base_path, "AppStrings.resx"), entries_en)
add_to_resx(os.path.join(base_path, "AppStrings.zh.resx"), entries_zh)
add_to_resx(os.path.join(base_path, "AppStrings.ja.resx"), entries_ja)
print("Added slide localized keys successfully.")
