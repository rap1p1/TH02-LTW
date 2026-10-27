// =====================================================
// app.js - Giao tiep Frontend voi Backend qua Axios
// Su dung cac phuong thuc: GET, POST, PUT, DELETE
// =====================================================

// Dia chi Backend API
const API_URL = "http://localhost:5000/api";

// Hien thi thong bao
function hienThiThongBao(noidung, loai = "ok") {
  const el = document.getElementById("thong-bao");
  el.textContent = noidung;
  el.className = "thong-bao " + loai;
  setTimeout(() => { el.className = "thong-bao"; }, 3000);
}

// Chuyen tab
function chuyenTab(tab) {
  document.querySelectorAll(".tab-content").forEach(el => el.classList.remove("active"));
  document.querySelectorAll(".tab").forEach(el => el.classList.remove("active"));
  document.getElementById("tab-" + tab).classList.add("active");
  event.target.classList.add("active");
  if (tab === "comics") taiDanhSachComic();
  if (tab === "don-hang") taiDanhSachDon();
}

// ======================
// COMIC - CRUD voi Axios
// ======================

// GET: Lay danh sach tat ca comics
async function taiDanhSachComic() {
  try {
    const response = await axios.get(`${API_URL}/comics`);
    const comics = response.data;
    const tbody = document.getElementById("ds-comics");
    if (comics.length === 0) {
      tbody.innerHTML = '<tr><td colspan="7" style="text-align:center">Chua co du lieu</td></tr>';
      return;
    }
    tbody.innerHTML = comics.map(c => `
      <tr>
        <td>${c.id}</td>
        <td>${c.title}</td>
        <td>${c.author}</td>
        <td>${c.genre}</td>
        <td>${c.price.toLocaleString()} VND</td>
        <td>${c.stock}</td>
        <td>
          <button class="btn btn-warning" onclick="suaComic(${c.id})">Sua (PUT)</button>
          <button class="btn btn-danger" onclick="xoaComic(${c.id})">Xoa (DELETE)</button>
        </td>
      </tr>
    `).join("");
  } catch (err) {
    hienThiThongBao("Loi khi tai danh sach: " + (err.response?.data?.message || err.message), "loi");
  }
}

// POST / PUT: Luu comic (them moi hoac cap nhat)
async function luuComic() {
  const id = document.getElementById("comic-id").value;
  const comic = {
    title:       document.getElementById("f-ten").value.trim(),
    author:      document.getElementById("f-tacgia").value.trim(),
    genre:       document.getElementById("f-theloai").value.trim(),
    price:       parseFloat(document.getElementById("f-gia").value) || 0,
    stock:       parseInt(document.getElementById("f-tonkho").value) || 0,
    description: document.getElementById("f-mota").value.trim()
  };

  if (!comic.title || !comic.author) {
    hienThiThongBao("Vui long nhap ten truyen va tac gia", "loi");
    return;
  }

  try {
    if (id) {
      // PUT: Cap nhat comic da co
      comic.id = parseInt(id);
      await axios.put(`${API_URL}/comics/${id}`, comic);
      hienThiThongBao("Da cap nhat truyen thanh cong (PUT)");
    } else {
      // POST: Them comic moi
      await axios.post(`${API_URL}/comics`, comic);
      hienThiThongBao("Da them truyen moi thanh cong (POST)");
    }
    xoaForm();
    taiDanhSachComic();
  } catch (err) {
    hienThiThongBao("Loi khi luu: " + (err.response?.data?.message || err.message), "loi");
  }
}

// GET: Lay thong tin comic de sua
async function suaComic(id) {
  try {
    const response = await axios.get(`${API_URL}/comics/${id}`);
    const c = response.data;
    document.getElementById("comic-id").value = c.id;
    document.getElementById("f-ten").value = c.title;
    document.getElementById("f-tacgia").value = c.author;
    document.getElementById("f-theloai").value = c.genre;
    document.getElementById("f-gia").value = c.price;
    document.getElementById("f-tonkho").value = c.stock;
    document.getElementById("f-mota").value = c.description || "";
    document.getElementById("tieu-de-form").textContent = "Cap nhat truyen (ID: " + id + ")";
    window.scrollTo(0, 0);
  } catch (err) {
    hienThiThongBao("Loi khi lay thong tin: " + err.message, "loi");
  }
}

// DELETE: Xoa comic
async function xoaComic(id) {
  if (!confirm("Ban co chac muon xoa truyen ID " + id + "?")) return;
  try {
    await axios.delete(`${API_URL}/comics/${id}`);
    hienThiThongBao("Da xoa truyen thanh cong (DELETE)");
    taiDanhSachComic();
  } catch (err) {
    hienThiThongBao("Loi khi xoa: " + (err.response?.data?.message || err.message), "loi");
  }
}

// Dat lai form ve trang thai ban dau
function xoaForm() {
  document.getElementById("comic-id").value = "";
  document.getElementById("f-ten").value = "";
  document.getElementById("f-tacgia").value = "";
  document.getElementById("f-theloai").value = "";
  document.getElementById("f-gia").value = "";
  document.getElementById("f-tonkho").value = "";
  document.getElementById("f-mota").value = "";
  document.getElementById("tieu-de-form").textContent = "Them truyen moi";
}

// =========================
// DON HANG - CRUD voi Axios
// =========================

// GET: Lay danh sach don hang
async function taiDanhSachDon() {
  try {
    const response = await axios.get(`${API_URL}/orders`);
    const orders = response.data;
    const tbody = document.getElementById("ds-don-hang");
    if (orders.length === 0) {
      tbody.innerHTML = '<tr><td colspan="6" style="text-align:center">Chua co don hang</td></tr>';
      return;
    }
    tbody.innerHTML = orders.map(o => `
      <tr>
        <td>${o.id}</td>
        <td>${o.customerName}</td>
        <td>${o.customerEmail}</td>
        <td>${o.totalPrice.toLocaleString()} VND</td>
        <td>${o.status}</td>
        <td>
          <button class="btn btn-warning" onclick="capNhatTrangThai(${o.id}, '${o.customerName}', '${o.customerEmail}', ${o.totalPrice})">Duyet (PUT)</button>
          <button class="btn btn-danger" onclick="xoaDon(${o.id})">Xoa (DELETE)</button>
        </td>
      </tr>
    `).join("");
  } catch (err) {
    hienThiThongBao("Loi khi tai don hang: " + (err.response?.data?.message || err.message), "loi");
  }
}

// POST: Tao don hang moi
async function taodonHang() {
  const don = {
    customerName:  document.getElementById("o-ten").value.trim(),
    customerEmail: document.getElementById("o-email").value.trim(),
    totalPrice:    parseFloat(document.getElementById("o-dongia").value) || 0,
    status:        "Cho xu ly",
    items: [{
      comicId:  parseInt(document.getElementById("o-comic-id").value) || 0,
      quantity: parseInt(document.getElementById("o-soluong").value) || 1,
      unitPrice: parseFloat(document.getElementById("o-dongia").value) || 0
    }]
  };

  if (!don.customerName || !don.customerEmail) {
    hienThiThongBao("Vui long nhap ten va email khach hang", "loi");
    return;
  }

  try {
    await axios.post(`${API_URL}/orders`, don);
    hienThiThongBao("Dat hang thanh cong (POST)");
    taiDanhSachDon();
  } catch (err) {
    hienThiThongBao("Loi khi dat hang: " + (err.response?.data?.message || err.message), "loi");
  }
}

// PUT: Cap nhat trang thai don hang
async function capNhatTrangThai(id, ten, email, tongTien) {
  const trangThaiMoi = prompt("Nhap trang thai moi:", "Da duyet");
  if (!trangThaiMoi) return;
  try {
    await axios.put(`${API_URL}/orders/${id}`, {
      id: id,
      customerName:  ten,
      customerEmail: email,
      totalPrice:    tongTien,
      status:        trangThaiMoi
    });
    hienThiThongBao("Da cap nhat trang thai don hang (PUT)");
    taiDanhSachDon();
  } catch (err) {
    hienThiThongBao("Loi khi cap nhat: " + (err.response?.data?.message || err.message), "loi");
  }
}

// DELETE: Xoa don hang
async function xoaDon(id) {
  if (!confirm("Ban co chac muon xoa don hang ID " + id + "?")) return;
  try {
    await axios.delete(`${API_URL}/orders/${id}`);
    hienThiThongBao("Da xoa don hang thanh cong (DELETE)");
    taiDanhSachDon();
  } catch (err) {
    hienThiThongBao("Loi khi xoa don: " + (err.response?.data?.message || err.message), "loi");
  }
}

// Khoi dong: tai danh sach truyen khi mo trang
window.onload = taiDanhSachComic;
