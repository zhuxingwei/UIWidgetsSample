using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIWidgetsSample {
    /**
     *
     *    The Get-Started Codes used in UIWidgets Tutorial
     *    You can run each sample cases by setting the sampleCaseId to 0 - 7 respectively
     *    While running the samples, please make sure:
     *     (1) You have attached this script to an UI Panel game object, which is parented by a Canvas game object
     *     (2) You have set the UI scale to 1x
     *
     */
    public class TutorialSample : UIWidgetsSamplePanel {

        List<Widget> sampleCases;

        //Choose a sample case to run
        const int sampleCaseId = 7;


        void initAllSamples() {
            if (this.sampleCases != null) {
                return;
            }

            this.sampleCases = new List<Widget> {
                this.createEmptyUI(),
                this.createTextLabelUI(),
                this.createBlackPictureUI(),
                this.createWhitePictureUI(),
                this.createUIWidgetLogo(),
                this.createComplexAppUI(),
                this.createComplexAppUIWithDifferentLayoutSetting(),
                this.createInteractiveAppUI()
            };
        }

        /**
         *  Create an empty UI 
         */
        Widget createEmptyUI() {
            return null;
        }
        
        /**
         * Create a text label UI
         */
        Widget createTextLabelUI() {
            return new Text("Hello UIWidgets");
        }
        
        /**
         * Create a "black-unity icon" picture onto the UI
         */
        Widget createBlackPictureUI() {
            return new Container(
                decoration: new BoxDecoration(
                    image: new DecorationImage(
                        image: new AssetImage(
                            "unity-black"
                            )
                        )
                    )
                );
        }
        
        
        /**
         * Create a "white-unity icon" picture onto the UI
         */
        Widget createWhitePictureUI() {
            return new Container(
                decoration: new BoxDecoration(
                    image: new DecorationImage(
                        image: new AssetImage(
                            "unity-white"
                        )
                    )
                )
            );
        }
        
        /**
         *  Create a UIWidgets logo on UI by combining a picture and a text label with Column Widgets
         */
        Widget createUIWidgetLogo() {
            return new Column(
                children: new List<Widget> {
                    new Container(
                        width: 100,
                        height: 100,
                        decoration: new BoxDecoration(
                            image: new DecorationImage(
                                image: new AssetImage(
                                    "unity-white")
                                )
                            )
                    ),
                    new Text("Hello UIWidgets")
                }
            );
        }
        
        /**
         *  Create a complex App UI
         */
        Widget createComplexAppUI() {
            return new MaterialApp(
                home: new Scaffold(
                    appBar: new AppBar(
                        title: new Text("Pick Your Transportation Mode")
                    ),
                    body: new Padding(
                        padding: EdgeInsets.all(16.0f),
                        child: new Card(
                            color: Colors.white,
                            child: new Center(
                                child: new Column(
                                    mainAxisSize: MainAxisSize.max,
                                    crossAxisAlignment: CrossAxisAlignment.center,
                                    mainAxisAlignment: MainAxisAlignment.center,
                                    children: new List<Widget> {
                                        new Icon(Unity.UIWidgets.material.Icons.directions_car, size: 128.0f),
                                        new RaisedButton(
                                            child: new Text("Next Mode"))
                                    }
                                )
                            )
                        )
                    ),
                    floatingActionButton: new FloatingActionButton(
                        backgroundColor: Colors.redAccent,
                        child: new Icon(Unity.UIWidgets.material.Icons.add_alert),
                        onPressed: () => { }
                    )
                )
            );
        }
        
        
        /**
         *  Create a complex App UI with a different layout setting:
         *  (1) align the icon and button horizontally
         *  (2) fill the space between the icon and button with white-spaces
         */
        Widget createComplexAppUIWithDifferentLayoutSetting() {
            return new MaterialApp(
                home: new Scaffold(
                    appBar: new AppBar(
                        title: new Text("Pick Your Transportation Mode")
                    ),
                    body: new Padding(
                        padding: EdgeInsets.all(16.0f),
                        child: new Card(
                            color: Colors.white,
                            child: new Center(
                                child: new Row(            //Column => Row
                                    mainAxisSize: MainAxisSize.max,
                                    crossAxisAlignment: CrossAxisAlignment.center,
                                    mainAxisAlignment: MainAxisAlignment.spaceBetween, // MainAxisAlignment.center => MainAxisAlignment.spaceBetween
                                    children: new List<Widget> {
                                        new Icon(Unity.UIWidgets.material.Icons.directions_car, size: 128.0f),
                                        new RaisedButton(
                                            child: new Text("Next Mode"))
                                    }
                                )
                            )
                        )
                    ),
                    floatingActionButton: new FloatingActionButton(
                        backgroundColor: Colors.redAccent,
                        child: new Icon(Unity.UIWidgets.material.Icons.add_alert),
                        onPressed: () => { }
                    )
                )
            );
        }
        
        /**
         *  Add Interaction logic to the App UI
         */
        Widget createInteractiveAppUI() {
            return new TransportationWidget();
        }
        
        
        class TransportationWidget : StatefulWidget
        {
            public TransportationWidget(Key key = null) : base(key) {
            }
 
            public override State createState() {
                return new TransportationWidgetState();
            }
        }

        class TransportationWidgetState : State<TransportationWidget> {
            int choice;

            readonly List<IconData> transportationIcons = new List<IconData> {
                Unity.UIWidgets.material.Icons.directions_car,
                Unity.UIWidgets.material.Icons.directions_bus,
                Unity.UIWidgets.material.Icons.directions_railway,
                Unity.UIWidgets.material.Icons.directions_walk
            };

            public override Widget build(BuildContext context) {
                return
                    new MaterialApp(
                        home: new Scaffold(
                            appBar: new AppBar(
                                title: new Text("Pick Your Transportation Mode")
                            ),
                            body: new Padding(
                                padding: EdgeInsets.all(16.0f),
                                child: new Card(
                                    color: Colors.white,
                                    child: new Center(
                                        child: new Column(
                                            mainAxisSize: MainAxisSize.min,
                                            crossAxisAlignment: CrossAxisAlignment.center,
                                            children: new List<Widget> {
                                                new Icon(this.transportationIcons[this.choice], size: 128.0f),
                                                new RaisedButton(
                                                    child: new Text("Next Mode"),
                                                    onPressed: () => {                    //interaction Logic add here
                                                        this.setState(() => {
                                                            this.choice =
                                                                (this.choice + 1) % this.transportationIcons.Count;
                                                        }
                                                    );
                                                    }
                                                )
                                            }
                                        )
                                    )
                                )
                            ),
                            floatingActionButton: new FloatingActionButton(
                                backgroundColor: Colors.redAccent,
                                child: new Icon(Unity.UIWidgets.material.Icons.add_alert),
                                onPressed: () => { }
                            )
                        )
                    );
            }
        }

        
        protected override Widget createWidget() {
            this.initAllSamples();
            return this.sampleCases[sampleCaseId];
        }

        protected override void OnEnable() {
            base.OnEnable();
            FontManager.instance.addFont(Resources.Load<Font>("MaterialIcons-Regular"), "Material Icons");
        }
    }
}
