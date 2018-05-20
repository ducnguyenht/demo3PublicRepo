import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:cached_network_image/cached_network_image.dart';

import '../pages/product.dart';
import '../models/product.dart';

import '../services/cart_service.dart';

class WidgetProductCard extends StatefulWidget {
  final ProductSummary product;
  final formatter = new NumberFormat("#,###");

  @override
  WidgetProductCardState createState() {
    return new WidgetProductCardState();
  }

  WidgetProductCard(this.product);
}

class WidgetProductCardState extends State<WidgetProductCard>
    with TickerProviderStateMixin {
  AnimationController _controller;
  Animation<double> _frontScale;
  Animation<double> _backScale;

  void flipCard() {
    setState(() {
      if (_controller.isCompleted || _controller.velocity > 0)
        _controller.reverse();
      else
        _controller.forward();
    });
  }

  @override
  void initState() {
    super.initState();
    _controller = new AnimationController(
      vsync: this,
      duration: const Duration(seconds: 1),
    );
    _frontScale = new Tween(
      begin: 1.0,
      end: 0.0,
    ).animate(new CurvedAnimation(
      parent: _controller,
      curve: new Interval(0.0, 0.5, curve: Curves.easeIn),
    ));
    _backScale = new CurvedAnimation(
      parent: _controller,
      curve: new Interval(0.5, 1.0, curve: Curves.easeOut),
    );
  }

  void goToProductPage(int productId, String productName) {
    Navigator.of(context).push(new MaterialPageRoute<Null>(
      builder: (BuildContext context) {
        return new PageProduct(productId, productName);
      },
    ));
  }

  void buyNowPressed(
      BuildContext context, String productName, int productId) async {
    var cartSvc = new ApiCartService();
    await cartSvc.addProductToCart(productId);
    var snackBar =
        new SnackBar(content: new Text('Đã thêm "$productName" vào giỏ hàng.'));
    Scaffold.of(context).showSnackBar(snackBar);
  }

  @override
  Widget build(BuildContext context) {
    return new Stack(children: <Widget>[
      new AnimatedBuilder(
          child: new GestureDetector(
              child: new Card(
                  child: new Container(
                      padding: new EdgeInsets.all(4.0),
                      child: new Column(
                        crossAxisAlignment: CrossAxisAlignment.stretch,
                        mainAxisSize: MainAxisSize.max,
                        children: <Widget>[
                          new Expanded(
                              child: new Text(
                            this.widget.product.name,
                            textAlign: TextAlign.center,
                            style: Theme.of(context).textTheme.subhead,
                          )),
                          new RaisedButton(
                              color: Theme.of(context).accentColor,
                              textTheme: ButtonTextTheme.primary,
                              onPressed: () => buyNowPressed(
                                  context,
                                  this.widget.product.name,
                                  this.widget.product.id),
                              child: new Row(children: <Widget>[
                                new Icon(Icons.add_shopping_cart),
                                new Container(width: 4.0),
                                new Text("Thêm vào giỏ")
                              ])),
                          new Container(height: 8.0),
                          new RaisedButton(
                              color: Theme.of(context).buttonColor,
                              textTheme: ButtonTextTheme.primary,
                              onPressed: () => goToProductPage(
                                  this.widget.product.id,
                                  this.widget.product.name),
                              child: new Row(children: <Widget>[
                                new Icon(Icons.info),
                                new Container(width: 4.0),
                                new Text("Xem chi tiết")
                              ]))
                        ],
                      ))),
              onTapUp: (tapDetail) => flipCard()),
          animation: _backScale,
          builder: (BuildContext context, Widget child) {
            final Matrix4 transform = new Matrix4.identity()
              ..scale(1.0, _backScale.value, 1.0);
            return new Transform(
              transform: transform,
              alignment: FractionalOffset.center,
              child: child,
            );
          }),
      new AnimatedBuilder(
        child: new GestureDetector(
            child: new Card(
                child: new Container(
                    padding: new EdgeInsets.all(4.0),
                    child: new Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        new Flexible(
                            child: new Center(
                              child: this.widget.product.imageUrl != null
                                  ? new CachedNetworkImage(
                                      imageUrl: this.widget.product.imageUrl,
                                      placeholder:
                                          new CircularProgressIndicator(),
                                      errorWidget: new Icon(Icons.error))
                                  : new Image.asset(
                                      'images/image_coming_soon.png',
                                      fit: BoxFit.cover),
                            ),
                            fit: FlexFit.loose,
                            flex: 4),
                        new Container(height: 16.0),
                        new SingleChildScrollView(
                            child: new Text(
                              this.widget.product.name,
                              style: Theme.of(context).textTheme.subhead,
                            ),
                            scrollDirection: Axis.horizontal),
                        new Divider(),
                        new Text(
                            '${this.widget.formatter.format(
                                this.widget.product.price)} đ',
                            style: Theme.of(context).textTheme.body2),
                      ],
                    ))),
            onTapUp: (tapDetail) => flipCard()),
        animation: _frontScale,
        builder: (BuildContext context, Widget child) {
          final Matrix4 transform = new Matrix4.identity()
            ..scale(1.0, _frontScale.value, 1.0);
          return new Transform(
            transform: transform,
            alignment: FractionalOffset.center,
            child: child,
          );
        },
      )
    ]);
  }
}
