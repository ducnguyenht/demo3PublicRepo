class Config {
  String baseUrl;
  int retailerId;
  int branchId;
  int saleChannelId;
  int soldById;
  String tokenUserName;
  String tokenPassword;

  String getFullUrl(String path) {
    return this.baseUrl + path;
  }

  Config(this.baseUrl, this.retailerId, this.branchId, this.saleChannelId,
      this.soldById, this.tokenUserName, this.tokenPassword);

  factory Config.getTestConfig() {
    var config = new Config(
        "https://itpt.kiotviet.com",
        276191, // itpt
        45950, // Chi nhanh trung tam
        25542, // Ban hang dien thoai
        133583,
        "01263553934",
        "123"); // Vo Van Trieu
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
        "@vanlinh123"); // Anh Linh
    return config;
  }
}
