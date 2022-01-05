import React,{useState} from "react";
import { useForm } from "react-hook-form";
import { Link,Redirect } from "react-router-dom";
import { useHistory } from "react-router-dom";
import {
  CButton,
  CCard,
  CCardBody,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CInputGroup,
  CInputGroupText,
  CRow,
} from "@coreui/react";
import CallAPI from "./../../../utils/Callapi";
import CIcon from "@coreui/icons-react";
import { cilLockLocked, cilUser } from "@coreui/icons";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const Register = () => {
  const history = useHistory();
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },

  } = useForm();
  const onSubmit = (data) => {
    console.log(data);
    CallAPI("register", "POST", data)
      .then((response) => {
        var data = response.data.data;
        toast("🦄 Register Success", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
        // userHasAuthenticated(true);
        history.push("/login");
        // setIsLogin(true);
      })
      .catch((error) => {
        console.log("Registration failed" + error);
        toast.error("🦄 Registration failed", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
      });
  };
  return (
    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={6}>
            <CCard className="mx-4">
              <CCardBody className="p-4">
                <CForm onSubmit={handleSubmit(onSubmit)}>
                  <h1>Register</h1>
                  <p className="text-medium-emphasis">Create your account</p>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput placeholder="Username" autoComplete="username"
                    {...register("userName")}
                    placeholder="Username"
                    autoComplete="userName"/>
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput placeholder="firstName" autoComplete="firstname"
                    {...register("firstName")}
                    placeholder="FirstName"
                    autoComplete="firstName"/>
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput placeholder="lastName" autoComplete="lastname"
                     {...register("lastName")}
                    placeholder="LastName"
                    autoComplete="lastName"/>
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>@</CInputGroupText>
                    <CFormInput placeholder="Email" autoComplete="email"
                    {...register("email")}
                    placeholder="Email"
                    autoComplete="email" />
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilLockLocked} />
                    </CInputGroupText>
                    <CFormInput
                    {...register("password")}
                      type="password"
                      placeholder="Password"
                      autoComplete="password"
                    />
                  </CInputGroup>
                  <CInputGroup className="mb-4">
                    <CInputGroupText>
                      <CIcon icon={cilLockLocked} />
                    </CInputGroupText>
                    <CFormInput
                    {...register("confirmPassword")}
                      type="password"
                      placeholder="Repeat password"
                      autoComplete="confirmPassword"
                    />
                  </CInputGroup>
                  <div className="d-grid">
                    <CButton type="submit" color="success">Create Account</CButton>
                  </div>
                </CForm>
              </CCardBody>
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Register
