using System;
using System.Collections.Generic;
using com.unity.uiwidgets.Runtime.rendering;
using UIWidgetsSample;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
using Icons = UIWidgetsSample.Icons;

namespace DefaultNamespace
{
    public class TestScrollController : UIWidgetsSamplePanel
    {
        protected override Widget createWidget()
        {
            return new MaterialApp(
                home: new CustomScrollViewTestRoute());
        }

        private class CustomScrollViewTestRoute : StatelessWidget
        {
            public override Widget build(BuildContext context)
            {
                return new Material(
                    child: new CustomScrollView(
                        slivers: new List<Widget>
                        {
                            new SliverAppBar(
                                pinned: true,
                                expandedHeight: 250.0f,
                                flexibleSpace: new FlexibleSpaceBar(
                                    title: new Text("Demo"),
                                    background: Image.asset(
                                    //The image path is different from https://book.flutterchina.club/chapter6/custom_scrollview.html
                                        "india_thanjavur_market", fit: BoxFit.cover)
                                )
                            ),

                            new SliverPadding(
                                padding: EdgeInsets.all(8.0f),
                                sliver: new SliverGrid( //Grid
                                    gridDelegate: new SliverGridDelegateWithFixedCrossAxisCount(
                                        crossAxisCount: 2, //Grid按两列显示
                                        mainAxisSpacing: 10.0f,
                                        crossAxisSpacing: 10.0f,
                                        childAspectRatio: 4.0f
                                    ),
                                    layoutDelegate: new SliverChildBuilderDelegate(
                                        (BuildContext subContext, int subIndex) =>
                                        {
                                            //创建子widget      
                                            return new Container(
                                                alignment: Alignment.center,
                                                //There is a minor problem in https://book.flutterchina.club/chapter6/custom_scrollview.html,
                                                //where the index could be 0, which leads to error since Colors.cyan (A dictionary) doesn't 
                                                //have such a key.
                                                color: Colors.cyan[100 * (1 + subIndex % 8)],
                                                child: new Text($"grid item {subIndex}")
                                            );
                                        },
                                        childCount: 20
                                    )
                                )
                            ),
                            //List
                            new SliverFixedExtentList(
                                itemExtent: 50.0f,
                                del: new SliverChildBuilderDelegate(
                                    (BuildContext subContext, int subIndex) =>
                                    {
                                        //创建列表项      
                                        return new Container(
                                            alignment: Alignment.center,
                                            //There is a minor problem in https://book.flutterchina.club/chapter6/custom_scrollview.html,
                                            //where the index could be 0, which leads to error since Colors.lightBlue (A dictionary) doesn't 
                                            //have such a key.
                                            color: Colors.lightBlue[100 * (1 + subIndex % 8)],
                                            child: new Text($"list item {subIndex}")
                                        );
                                    },
                                    childCount: 50 //50个列表项
                                )
                            )
                        }
                    )
                );
            }
        }

        private class ScrollControllerTestRoute : StatefulWidget
        {
            public override State createState()
            {
                return new ScrollControllerTestRouteState();
            }
        }

        private class ScrollControllerTestRouteState : State<ScrollControllerTestRoute>
        {
            private ScrollController _controller = new ScrollController();
            private bool showToTopBtn = false; //是否显示“返回到顶部”按钮

            public override void initState()
            {
                //监听滚动事件，打印滚动位置
                _controller.addListener(() =>
                {
                    print(_controller.offset); //打印滚动位置
                    if (_controller.offset < 1000 && showToTopBtn)
                        setState(() => { showToTopBtn = false; });
                    else if (_controller.offset >= 1000 && showToTopBtn == false)
                        setState(() => { showToTopBtn = true; });
                });
            }

            public override void dispose()
            {
                //为了避免内存泄露，需要调用_controller.dispose
                _controller.dispose();
                base.dispose();
            }

            public override Widget build(BuildContext context)
            {
                return new Scaffold(
                    appBar: new AppBar(title: new Text("滚动控制")),
                    body: new Scrollbar(
                        child: ListView.builder(
                            itemCount: 100,
                            itemExtent: 50.0f, //列表项高度固定时，显式指定高度是一个好习惯(性能消耗小)
                            controller: _controller,
                            itemBuilder: (SubContext, index) => { return new ListTile(title: new Text($"{index}")); }
                        )
                    ),
                    floatingActionButton: !showToTopBtn
                        ? null
                        : new FloatingActionButton(
                            child: new Icon(Icons.account_circle),
                            onPressed: () =>
                            {
                                //返回到顶部时执行动画
                                _controller.animateTo(0f,
                                    duration: new TimeSpan(0, 0, 0, 0, 200),
                                    curve: Curves.ease
                                );
                            }
                        )
                );
            }
        }
    }
}
