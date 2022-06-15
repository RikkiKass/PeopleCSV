import React, { useState, useEffect } from "react";
import axios from "axios";


const Home = () => {

    const [people, setPeople] = useState([]);

    useEffect(() => {
        const getPeople = async () => {
            const { data } = await axios.get('/api/peoplecsv/getpeople');
            setPeople(data);
        }
        getPeople();
    }, []);

    const onDeleteClick = async () => {
        await axios.post('/api/peoplecsv/deleteall');
        setPeople([]);
    }

    return (
        <div className="container">
            <button className=" btn btn-danger btn-block btn-log" onClick={onDeleteClick}>
                Delete All People
            </button>
            <table className="table table-bordered table-hover table-striped">
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Email</th>
                        <th>Address</th>
                    </tr>
                </thead>
                <tbody>
                    {people.map(p => <tr>
                        <td>{p.firstName}</td>
                        <td>{p.lastName}</td>
                        <td>{p.age}</td>
                        <td>{p.email}</td>
                        <td>{p.address}</td>
                    </tr>)}

                </tbody>
            </table>
        </div>
    )
}
export default Home;