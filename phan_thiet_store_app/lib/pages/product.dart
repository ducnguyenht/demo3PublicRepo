import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:flutter_advanced_networkimage/flutter_advanced_networkimage.dart';
import 'package:flutter_advanced_networkimage/transition_to_image.dart';
import 'package:flutter_markdown/flutter_markdown.dart';
import 'package:html2md/html2md.dart' as html2md;

import '../models/product.dart';

import '../services/product_service.dart';
import '../services/cart_service.dart';

class PageProduct extends StatefulWidget {
  final int productId;
  final formatter = new NumberFormat("#,###");

  PageProduct(this.productId);

  @override
  PageProductState createState() {
    return new PageProductState(this.productId);
  }
}

class PageProductState extends State<PageProduct> {
  int productId;

  PageProductState(this.productId);
  
  void buyNowPressed(BuildContext bContext, String productName) {
    var cartSvc = new MockCartService();
    cartSvc.addProductToCart(productId);
    var snackBar = new SnackBar(
        content: new Text('Đã thêm "$productName" vào giỏ hàng.'));
    Scaffold.of(bContext).showSnackBar(snackBar);
  }

  @override
  Widget build(BuildContext context) {
    var productSvc = new ApiProductService();
    return new FutureBuilder<ProductDetail>(
      future: productSvc.getProductById(productId),
      builder: (context, snapshot) {
        if (snapshot.hasError) print(snapshot.error);

        return snapshot.hasData
            ? new Scaffold(
            appBar: new AppBar(title: new Text(snapshot.data.name)),
            body: new Builder(
              // Create an inner BuildContext so that the onPressed methods
              // can refer to the Scaffold with Scaffold.of().
                builder: (BuildContext context) {
                  return new Container(
                    child: new Column(
                      crossAxisAlignment: CrossAxisAlignment.stretch,
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        new Flexible(child: new Center(
                            child: snapshot.data.imageUrl != null
                                ? new TransitionToImage(new AdvancedNetworkImage(
                                snapshot.data.imageUrl,
                                useDiskCache: true))
                                : new Image.asset('images/image_coming_soon.png',
                                fit: BoxFit.cover))
                        )
                        ,
                        new Text(snapshot.data.name),
                        new Text(
                            '${widget.formatter.format(snapshot.data.price)} đ'),
                        new Text('Thông số sản phẩm',
                            style: new TextStyle(fontWeight: FontWeight.bold)),
                        new Expanded(
                            child: new Markdown(data: html2md.convert(snapshot.data.description))
                        ),
                        new Container(
                            padding: new EdgeInsets.all(10.0),
                            child: new FlatButton.icon(
                                onPressed: () => buyNowPressed(context, snapshot.data.name),
                                icon: new Icon(Icons.add_shopping_cart),
                                label: new Text('Mua ngay'),
                                color: new Color.fromARGB(255, 244, 0, 0),
                                textColor: new Color.fromARGB(255, 255, 255, 255),
                                highlightColor: new Color.fromARGB(255, 200, 0, 0))),
                      ],
                    ),
                    padding: new EdgeInsets.all(10.0),
                  );
                }))
            : new Center(child: new CircularProgressIndicator());
      },
    );
  }
}
