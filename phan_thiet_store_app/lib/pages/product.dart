import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter_markdown/flutter_markdown.dart';
import 'package:html2md/html2md.dart' as html2md;

import '../models/product.dart';

import '../services/product_service.dart';
import '../services/cart_service.dart';

class PageProduct extends StatelessWidget {
  final int productId;
  final String productName;

  final formatter = new NumberFormat("#,###");

  PageProduct(this.productId, this.productName);

  void buyNowPressed(BuildContext context, String productName) async {
    var cartSvc = new ApiCartService();
    await cartSvc.addProductToCart(productId);
    var snackBar =
        new SnackBar(content: new Text('Đã thêm "$productName" vào giỏ hàng.'));
    Scaffold.of(context).showSnackBar(snackBar);
  }

  @override
  Widget build(BuildContext context) {
    var productSvc = new ApiProductService();

    return new Scaffold(
        appBar: new AppBar(title: new Text(this.productName)),
        body: new FutureBuilder<ProductDetail>(
          future: productSvc.getProductById(productId),
          builder: (fBuilder, snapshot) {
            if (snapshot.hasError) print(snapshot.error);
            return snapshot.hasData
                ? new Container(
                    child: new Column(
                      crossAxisAlignment: CrossAxisAlignment.stretch,
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        new Flexible(
                            child: new Center(
                                child: snapshot.data.imageUrl != null
                                    ? new CachedNetworkImage(
                                        imageUrl: snapshot.data.imageUrl,
                                        placeholder:
                                            new CircularProgressIndicator(),
                                        errorWidget: new Icon(Icons.error))
                                    : new Image.asset(
                                        'images/image_coming_soon.png',
                                        fit: BoxFit.cover))),
                        new Text(snapshot.data.name),
                        new Text('${formatter.format(snapshot.data.price)} đ'),
                        new Text('Thông số sản phẩm',
                            style: new TextStyle(fontWeight: FontWeight.bold)),
                        new Divider(),
                        new Expanded(
                          child: new Scrollbar(
                              child: new SingleChildScrollView(
                            child: snapshot.data.description != null
                                ? new MarkdownBody(
                                    data: html2md
                                        .convert(snapshot.data.description))
                                : new Text('Sản phẩm chưa cập nhật thông tin'),
                          )),
                        ),
                      ],
                    ),
                    padding: new EdgeInsets.all(10.0),
                  )
                : new Center(child: new CircularProgressIndicator());
          },
        ),
        floatingActionButton:
            new Builder(builder: (BuildContext scaffoldContext) {
          return new FloatingActionButton(
              child: new Icon(Icons.add_shopping_cart,
                  color: new Color(0xff000000)),
              tooltip: 'Thêm vào giỏ hàng',
              onPressed: () => buyNowPressed(scaffoldContext, productName));
        }));
  }
}
