

window.onload = function () {
  let cart = [];
  if (localStorage.getItem('cart')) {
    cart = JSON.parse(localStorage.getItem('cart'));
    var cartnumber = document.getElementById("cartnumber");
    cartnumber.innerText = cart.length;
  }
};

function addToCart(productId, productNameE, productNameA, price, offer, imageUrl, quantity) {
  

  var cartnumber = document.getElementById("cartnumber");
  let cart = [];

  var item ;

  if (localStorage.getItem('cart')) {
    cart = JSON.parse(localStorage.getItem('cart'));
    item = cart.find(product => product.productId === productId);
    if (item != null)
    {
      addProductNotice('Product already in your cart', '', '', 'danger');
      $('#jGrowl').css({ 'color': 'red' });
    }
    else
    {
      cartnumber.innerText = cart.length + 1;
      cart.push({ 'productId': productId, 'productNameE': productNameE, 'productNameA': productNameA, 'price': price, 'offer': offer, 'imgUrl': imageUrl, 'quantity': quantity });
      localStorage.setItem('cart', JSON.stringify(cart));
      addProductNotice('Product added to Cart successfully', '', '', 'danger');
      $('#jGrowl').css({ 'color': '#0b601b' });
    }
    
  } else {
    cartnumber.innerText = 1;
    cart.push({ 'productId': productId, 'productNameE': productNameE, 'productNameA': productNameA, 'price': price, 'offer': offer, 'imgUrl': imageUrl, 'quantity': quantity });
    localStorage.setItem('cart', JSON.stringify(cart));
    addProductNotice('Product added to Cart successfully', '', '', 'danger');
    $('#jGrowl').css({ 'color': '#0b601b' });
  }

  
  /*addTable();*/
}

function addTable() {
 
  //var myTableDiv = document.getElementById("myDynamicTable");
  //myTableDiv.innerHTML = "";
  //var table = document.createElement('TABLE');
  //table.border = '1';

  var tableBody = document.getElementById('cartTableBody');
  tableBody.innerHTML = "";
  /*table.appendChild(tableBody);*/
  let cart = [];
  cart = JSON.parse(localStorage.getItem('cart'));
 
  if (cart.length > 0 ) {
    document.getElementById('isCart').style.display = "block";
    document.getElementById('noCart').style.display = "none";
    
    var subTot = 0;
    for (var i = 0; i < cart.length; i++) {
      var tr = document.createElement('TR');
      tableBody.appendChild(tr);
      var td1 = document.createElement('TD');
      td1.classList.add('text-center')
      td1.width = '75';
      var imgSrc = (cart[i].imgUrl).substring(1);
      td1.innerHTML = '<a href="/ClientProduct/GetProductDetails/' + cart[i].productId + '"><img hight="70px" width="70px" src="' + imgSrc + '" alt="Aspire Ultrabook Laptop" title="Aspire Ultrabook Laptop" class="img-thumbnail" /></a>';
      tr.appendChild(td1);
      var td2 = document.createElement('TD');
      //td2.width = '75';
      td2.classList.add('text-left');
      td2.innerHTML = ' <a href="/ClientProduct/GetProductDetails/' + cart[i].productId + '">' + cart[i].productNameE + '</a><br />';
      tr.appendChild(td2);
      var td3 = document.createElement('TD');
      td3.width = '200px';
      td3.innerHTML = ' <div class="input-group btn-block quantity-control quantity"> <span class="input-group-btn">    <button type="button" data-toggle="tooltip" title="Remove" class="btn btn-primary" onClick="editCartQuantity(' + cart[i].productId + ',0)"><i class="fa fa-minus-circle"></i></button>     </span> <input type= "text" name = "quantity" disabled style="min-width:45px" value = "' + cart[i].quantity + '" class="form-control" /><span class="input-group-btn">    <button type="button" data-toggle="tooltip" title="Remove" class="btn btn-primary" onClick="editCartQuantity(' + cart[i].productId + ',1)"><i class="fa fa-plus-circle"></i></button>     </span> <span class="input-group-btn">    <button type="button" data-toggle="tooltip" title="Remove" class="btn btn-danger" onClick="DeleteCartItem(' + cart[i].productId + ')"><i class="fa fa-times-circle"></i></button>     </span>  </div >'
      tr.appendChild(td3);
      var td4 = document.createElement('TD');
      //td4.width = '75';
      if (cart[i].offer > 0) {

        td4.innerHTML = '<div class="price"> <span class="price-new">' + cart[i].offer + ' LE</span> <span class="price-old">' + cart[i].price + ' LE</span></div> ';
      } else {
        td4.innerHTML = '<div class="price" > <span class="price-new" itemprop="price">' + cart[i].price + 'EGP</span></div>'

      }
      tr.appendChild(td4);
      var td5 = document.createElement('TD');
      //td6.width = '75';
      if (cart[i].offer > 0) {
        td5.appendChild(document.createTextNode(Number(cart[i].quantity) * Number(cart[i].offer)));
        subTot += Number(cart[i].quantity) * Number(cart[i].offer);
      }
      else {
        td5.appendChild(document.createTextNode(Number(cart[i].quantity) * Number(cart[i].price)));
        subTot += Number(cart[i].quantity) * Number(cart[i].price);
      }
      tr.appendChild(td5);

    }
    
    document.getElementById("subTotaltd").innerText = subTot + ' EGP';
/*    document.getElementById("Totaltd").innerText = subTot + ' EGP';*/
  }
  else
  {
    document.getElementById('isCart').style.display = 'none';
    document.getElementById('noCart').style.display = 'block';
  }
}

/*Checkout table*/

function addTableCheckout() {

  var tableBody = document.getElementById('cartTableBody');
  tableBody.innerHTML = "";
  let cart = [];
  cart = JSON.parse(localStorage.getItem('cart'));
  if (cart.length > 0) {
    var subTot = 0;
    for (var i = 0; i < cart.length; i++) {
      var tr = document.createElement('TR');
      tableBody.appendChild(tr);
      var td1 = document.createElement('TD');
      td1.classList.add('text-center')
      td1.width = '75';
      var imgSrc = (cart[i].imgUrl).substring(1);
      td1.innerHTML = '<a href="/ClientProduct/GetProductDetails/' + cart[i].productId + '"><img hight="70px" width="70px" src="' + imgSrc + '" alt="Aspire Ultrabook Laptop" title="Aspire Ultrabook Laptop" class="img-thumbnail" /></a>';
      tr.appendChild(td1);
      var td2 = document.createElement('TD');
      //td2.width = '75';
      td2.classList.add('text-left');
      td2.innerHTML = ' <a href="/ClientProduct/GetProductDetails/' + cart[i].productId + '">' + cart[i].productNameE + '</a><br />';
      tr.appendChild(td2);
      var td3 = document.createElement('TD');
      td3.width = '200px';
      td3.innerHTML = ' <div class="input-group btn-block quantity-control quantity"> <input type= "text" name = "quantity" value = "' + cart[i].quantity + '" size = "1" class="form-control" disabled />  </div >'
      tr.appendChild(td3);
      var td4 = document.createElement('TD');
      //td4.width = '75';
      if (cart[i].offer > 0) {

        td4.innerHTML = '<div class="price"> <span class="price-new">' + cart[i].offer + ' LE</span> <span class="price-old">' + cart[i].price + ' LE</span></div> ';
      } else {
        td4.innerHTML = '<div class="price" > <span class="price-new" itemprop="price">' + cart[i].price + 'EGP</span></div>'

      }
      tr.appendChild(td4);
      var td5 = document.createElement('TD');
      //td6.width = '75';
      if (cart[i].offer > 0) {
        td5.appendChild(document.createTextNode(Number(cart[i].quantity) * Number(cart[i].offer)));
        subTot += Number(cart[i].quantity) * Number(cart[i].offer);
      }
      else {
        td5.appendChild(document.createTextNode(Number(cart[i].quantity) * Number(cart[i].price)));
        subTot += Number(cart[i].quantity) * Number(cart[i].price);
      }
      tr.appendChild(td5);

    }
    document.getElementById("subTotaltd").innerText = subTot + ' EGP';
    document.getElementById("Totaltd").innerText = subTot + ' EGP';
  }
  else {
  }
}


function editCartQuantity(productId,increment) {
 

  if (localStorage.getItem('cart')) {
    let Cart = JSON.parse(localStorage.getItem('cart'));
    // let item = Cart.filter(product => product.productId === productId);
    var item = Cart.find(product => product.productId === productId);
    if (increment == "1") {
      ++item.quantity;
    }
    else
    {
      if (item.quantity > 1)
      item.quantity = item.quantity-1;
    }
    

    // Cart = Cart.filter(product => product.productId !== productId);
    // Cart.push({ 'productId': item.productId, 'productName':item.productName, 'price':item.price, 'imgUrl':item.imageUrl,'quantity':item.quantity });
    localStorage.setItem('cart', JSON.stringify(Cart));
    addTable();
  }
}

function DeleteCartItem(productId) {

  // Your logic for your app.

  // strore products in local storage

  let oldCart = JSON.parse(localStorage.getItem('cart'));
  let newCart = oldCart.filter(product => product.productId !== productId);
  localStorage.setItem('cart', JSON.stringify(newCart));
  addTable();
  
  cartnumber.innerText = newCart.length;


}



