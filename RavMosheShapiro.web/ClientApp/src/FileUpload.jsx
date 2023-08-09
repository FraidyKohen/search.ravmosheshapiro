import React, { useEffect, useState, useRef } from 'react';
import axios from 'axios';

const toBase64 = file => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

const FileUpload = () => {

    const [authorized, setAuthorized] = useState(false);
    const [inputPassword, setInputPassword] = useState('');


    const fileRef = useRef(null);

    const onPasswordChange = e => {
        setInputPassword(e.target.value);
    }
    const onPasswordSubmit = () => {
        if (inputPassword == 'Zya5765') {
            setAuthorized(true);
            console.log('authorized');
        }
    }

    const onUploadClick = async () => {
        const file = fileRef.current.files[0];
        const base64 = await toBase64(file);
        const { data } = await axios.post(`/api/search/Upload`, { base64, name: file.name });
        console.log(data);
    }

    if (!authorized) {
        return (<>
            <div className='col-md-4 offset-4 text-center'>
                <h1 className='text-danger'>Admin Only</h1>
                <input onChange={onPasswordChange} type='password' placeholder='Password' className='form-control' />
                <label className='form-label'>Please enter admin password</label>
                <br/>
                <button className='btn btn-secondary' disabled={inputPassword == ''} onClick={onPasswordSubmit}>Submit</button>
            </div>
        </>);
    }
    if (authorized) {
        return (
            <>
                <div className='row'>
                    <div className='col-md-6'>
                        <input ref={fileRef} type='file' className='form-control' />
                    </div>
                    <div className='col-md-6'>
                        <button className='btn btn-primary w-100' onClick={onUploadClick}>Upload</button>
                    </div>
                </div>
            </>
        );
    }

}

export default FileUpload;