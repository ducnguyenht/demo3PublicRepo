class Config {
  static const String env = "prod";

  String baseUrl;
  int retailerId;
  int branchId;
  int saleChannelId;
  int soldById;
  String tokenUserName;
  String tokenPassword;
  bool isSupportCheckout;
  String checkOutPath;

  String getFullUrl(String path) {
    return this.baseUrl + path;
  }

  Config(
      this.baseUrl,
      this.retailerId,
      this.branchId,
      this.saleChannelId,
      this.soldById,
      this.tokenUserName,
      this.tokenPassword,
      this.isSupportCheckout,
      this.checkOutPath);

  factory Config.getConfig() {
    if (env != "prod") {
      return new Config.getTestConfig();
    }

    return new Config.getProdConfig();
  }

  factory Config.getTestConfig() {
    var config = new Config(
        "https://itpt.kiotviet.com",
        276191, // itpt
        45950, // Chi nhanh trung tam
        25542, // Ban hang dien thoai
        133583,
        "01263553934",
        "123",
        false,
        ""); // Vo Van Trieu
    return config;
  }

  factory Config.getProdConfig() {
    var config = new Config(
        "https://nzt.kiotviet.com",
        244993, // Phu Kien Phan Thiet
        13933, // Chi nhanh LHP
        25552, // Ban hang dien thoai
        29657,
        "0944505039",
        "@vanlinh123",
        true,
        "http://phukienphanthiet.com/umbraco/api/PkPTApi/PostDatHangMainResponse"); // Anh Linh
    return config;
  }
}
