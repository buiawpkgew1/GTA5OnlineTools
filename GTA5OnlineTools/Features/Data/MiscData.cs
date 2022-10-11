namespace GTA5OnlineTools.Features.Data;

public static class MiscData
{
    public struct Blip
    {
        public string Name;
        public int ID;
    }

    public static List<Blip> Blips = new()
    {
        new Blip(){ Name="游艇", ID=455 },
        new Blip(){ Name="CEO办公室", ID=475 },
        new Blip(){ Name="摩托帮会所", ID=492 },
        new Blip(){ Name="大麻种植场", ID=496 },
        new Blip(){ Name="可卡因工厂", ID=497 },
        new Blip(){ Name="伪证件办公室", ID=498 },
        new Blip(){ Name="冰毒实验室", ID=499 },
        new Blip(){ Name="假钞工厂", ID=500 },
        new Blip(){ Name="地堡", ID=557 },
        new Blip(){ Name="机库", ID=569 },
        new Blip(){ Name="设施", ID=590 },
        new Blip(){ Name="夜总会", ID=614 },
        new Blip(){ Name="游戏厅", ID=740 },
        new Blip(){ Name="虎鲸", ID=760 },
        new Blip(){ Name="车友会", ID=777 },
    };

    public struct Session
    {
        public string Name;
        public int ID;
    }

    public static List<Session> Sessions = new()
    {
        new Session(){ Name="离开线上模式", ID=-1 },
        new Session(){ Name="公共战局", ID=0 },
        new Session(){ Name="创建公共战局", ID=1 },
        new Session(){ Name="私人帮会战局", ID=2 },
        new Session(){ Name="帮会战局", ID=3 },
        new Session(){ Name="加入好友", ID=9 },
        new Session(){ Name="私人好友战局", ID=6 },
        new Session(){ Name="单人战局", ID=10 },
        new Session(){ Name="仅限邀请战局", ID=11 },
        new Session(){ Name="加入帮会伙伴", ID=12 },
    };

    public struct RPxN
    {
        public string Name;
        public float ID;
    }

    public static List<RPxN> RPxNs = new()
    {
        new RPxN(){ Name="RPx1", ID=1.0f },
        new RPxN(){ Name="RPx2", ID=2.0f },
        new RPxN(){ Name="RPx5", ID=5.0f },
        new RPxN(){ Name="RPx10", ID=10.0f },
        new RPxN(){ Name="RPx20", ID=20.0f },
        new RPxN(){ Name="RPx50", ID=50.0f },
        new RPxN(){ Name="RPx100", ID=100.0f },
        new RPxN(){ Name="RPx500", ID=500.0f },
        new RPxN(){ Name="RPx1000", ID=1000.0f },
    };

    public struct REPxN
    {
        public string Name;
        public float ID;
    }

    public static List<REPxN> REPxNs = new()
    {
        new REPxN(){ Name="REPx1", ID=1.0f },
        new REPxN(){ Name="REPx2", ID=2.0f },
        new REPxN(){ Name="REPx5", ID=5.0f },
        new REPxN(){ Name="REPx10", ID=10.0f },
        new REPxN(){ Name="REPx20", ID=20.0f },
        new REPxN(){ Name="REPx50", ID=50.0f },
        new REPxN(){ Name="REPx100", ID=100.0f },
        new REPxN(){ Name="REPx500", ID=500.0f },
        new REPxN(){ Name="REPx1000", ID=1000.0f },
    };

    public struct CEOCargo
    {
        public string Name;
        public int ID;
    }

    // Set Global_1946791 to 1, otherwise you'll see regular crates.
    // Set Global_1946637 to one of these: 2, 4, 6, 7, 8, 9.
    // Now you'll see the unique cargo in your terrorbyte.

    public static List<CEOCargo> CEOCargos = new()
    {
        new CEOCargo(){ Name="医疗用品", ID=0 },
        new CEOCargo(){ Name="烟酒", ID=1 },
        new CEOCargo(){ Name="古董艺术品（华丽彩蛋）", ID=2 },
        new CEOCargo(){ Name="电子产品", ID=3 },
        new CEOCargo(){ Name="武器弹药（黄金火神机枪）", ID=4 },
        new CEOCargo(){ Name="迷幻药", ID=5 },
        new CEOCargo(){ Name="宝石（一大颗钻石）", ID=6 },
        new CEOCargo(){ Name="动物材料（稀有皮草）", ID=7 },
        new CEOCargo(){ Name="仿制品（电影胶卷）", ID=8 },
        new CEOCargo(){ Name="珠宝（稀有怀表）", ID=9 },
        new CEOCargo(){ Name="银块", ID=10 },
    };

    public struct MerryWeatherService
    {
        public string Name;
        public int ID;
    }

    public static List<MerryWeatherService> MerryWeatherServices = new()
    {
        new MerryWeatherService(){ Name="请求弹药空投", ID=874 },
        new MerryWeatherService(){ Name="请求重型防弹装甲", ID=884 },
        new MerryWeatherService(){ Name="请求牛鲨睾酮", ID=882 },
        new MerryWeatherService(){ Name="请求船只接送", ID=875 },
        new MerryWeatherService(){ Name="请求直升机接送", ID=876 },
    };

    public struct LocalWeather
    {
        public string Name;
        public int ID;
    }

    public static List<LocalWeather> LocalWeathers = new()
    {
        new LocalWeather(){ Name="默认", ID=-1 },
        new LocalWeather(){ Name="格外晴朗", ID=0 },
        new LocalWeather(){ Name="晴朗", ID=1 },
        new LocalWeather(){ Name="多云", ID=2 },
        new LocalWeather(){ Name="阴霾", ID=3 },
        new LocalWeather(){ Name="大雾", ID=4 },
        new LocalWeather(){ Name="阴天", ID=5 },
        new LocalWeather(){ Name="下雨", ID=6 },
        new LocalWeather(){ Name="雷雨", ID=7 },
        new LocalWeather(){ Name="雨转晴", ID=8 },
        new LocalWeather(){ Name="阴雨", ID=9 },
        new LocalWeather(){ Name="下雪", ID=10 },
        new LocalWeather(){ Name="暴雪", ID=11 },
        new LocalWeather(){ Name="小雪", ID=12 },
        new LocalWeather(){ Name="圣诞", ID=13 },
        new LocalWeather(){ Name="万圣节", ID=14 },
    };

    public struct ImpactExplosion
    {
        public string Name;
        public int ID;
    }

    public static List<ImpactExplosion> ImpactExplosions = new()
    {
        new ImpactExplosion(){ Name="默认", ID=-1 },
        new ImpactExplosion(){ Name="手榴弹", ID=0 },
        new ImpactExplosion(){ Name="榴弹发射器", ID=1 },
        new ImpactExplosion(){ Name="粘弹", ID=2 },
        new ImpactExplosion(){ Name="燃烧瓶", ID=3 },
        new ImpactExplosion(){ Name="火箭弹", ID=4 },
        new ImpactExplosion(){ Name="坦克炮弹", ID=5 },
        new ImpactExplosion(){ Name="火球爆炸", ID=6 },
        new ImpactExplosion(){ Name="汽车爆炸", ID=7 },
        new ImpactExplosion(){ Name="飞机爆炸", ID=8 },
        new ImpactExplosion(){ Name="加油站爆炸", ID=9 },
        new ImpactExplosion(){ Name="摩托车爆炸", ID=10 },
        new ImpactExplosion(){ Name="蒸汽", ID=11 },
        new ImpactExplosion(){ Name="火焰", ID=12 },
        new ImpactExplosion(){ Name="消防栓", ID=13 },
        new ImpactExplosion(){ Name="燃气罐", ID=14 },
        new ImpactExplosion(){ Name="小艇", ID=15 },
        new ImpactExplosion(){ Name="船只", ID=16 },
        new ImpactExplosion(){ Name="卡车爆炸", ID=17 },
        new ImpactExplosion(){ Name="MKⅡ爆炸子弹", ID=18 },
        new ImpactExplosion(){ Name="烟雾弹发射器", ID=19 },
        new ImpactExplosion(){ Name="烟雾弹", ID=20 },
        new ImpactExplosion(){ Name="毒气弹", ID=21 },
        new ImpactExplosion(){ Name="信号弹", ID=22 },
        new ImpactExplosion(){ Name="带特效的爆炸", ID=23 },
        new ImpactExplosion(){ Name="灭火器", ID=24 },
        new ImpactExplosion(){ Name="可调榴弹", ID=25 },
        new ImpactExplosion(){ Name="火车爆炸", ID=26 },
        new ImpactExplosion(){ Name="油桶", ID=27 },
        new ImpactExplosion(){ Name="丙烷", ID=28 },
        new ImpactExplosion(){ Name="飞艇", ID=29 },
        new ImpactExplosion(){ Name="火焰 ", ID=30 },
        new ImpactExplosion(){ Name="油罐车", ID=31 },
        new ImpactExplosion(){ Name="飞机导弹", ID=32 },
        new ImpactExplosion(){ Name="汽车导弹", ID=33 },
        new ImpactExplosion(){ Name="燃油泵", ID=34 },
        new ImpactExplosion(){ Name="鸟屎", ID=35 },
        new ImpactExplosion(){ Name="电磁步枪", ID=36 },
        new ImpactExplosion(){ Name="飞艇爆炸", ID=37 },
        new ImpactExplosion(){ Name="烟花发射器", ID=38 },
        new ImpactExplosion(){ Name="雪球", ID=39 },
        new ImpactExplosion(){ Name="地雷", ID=40 },
        new ImpactExplosion(){ Name="女武神机炮", ID=41 },
        new ImpactExplosion(){ Name="游艇防御", ID=42 },
        new ImpactExplosion(){ Name="爆破筒", ID=43 },
        new ImpactExplosion(){ Name="汽车炸弹", ID=44 },
        new ImpactExplosion(){ Name="MK2 定向导弹", ID=45 },
        new ImpactExplosion(){ Name="APC炮弹", ID=46 },
        new ImpactExplosion(){ Name="飞机集束炸弹", ID=47 },
        new ImpactExplosion(){ Name="飞机瓦斯", ID=48 },
        new ImpactExplosion(){ Name="飞机燃烧弹", ID=49 },
        new ImpactExplosion(){ Name="飞机炸弹", ID=50 },
        new ImpactExplosion(){ Name="鱼雷", ID=51 },
        new ImpactExplosion(){ Name="水下鱼雷", ID=52 },
        new ImpactExplosion(){ Name="邦布须卡炮", ID=53 },
        new ImpactExplosion(){ Name="第二集束炸弹", ID=54 },
        new ImpactExplosion(){ Name="猎杀者连发导弹", ID=55 },
        new ImpactExplosion(){ Name="FH-1 猎杀者 机炮", ID=56 },
        new ImpactExplosion(){ Name="流氓大炮", ID=57 },
        new ImpactExplosion(){ Name="水下地雷", ID=58 },
        new ImpactExplosion(){ Name="天基炮", ID=59 },
        new ImpactExplosion(){ Name="轨道炮", ID=60 },
        new ImpactExplosion(){ Name="MK2爆炸子弹霰弹枪", ID=61 },
        new ImpactExplosion(){ Name="MK2导弹", ID=62 },
        new ImpactExplosion(){ Name="武装坦帕迫击炮", ID=63 },
        new ImpactExplosion(){ Name="追踪地雷", ID=64 },
        new ImpactExplosion(){ Name="电磁脉冲地雷", ID=65 },
        new ImpactExplosion(){ Name="尖刺式地雷", ID=66 },
        new ImpactExplosion(){ Name="感应式地雷", ID=67 },
        new ImpactExplosion(){ Name="定时地雷", ID=68 },
        new ImpactExplosion(){ Name="无人机自爆", ID=69 },
        new ImpactExplosion(){ Name="原子能枪", ID=70 },
        new ImpactExplosion(){ Name="跳雷", ID=71 },
        new ImpactExplosion(){ Name="脚本化导弹", ID=72 },
        new ImpactExplosion(){ Name="潜艇导弹", ID=82 },
        new ImpactExplosion(){ Name="无伤害爆炸", ID=99 },
    };
}
