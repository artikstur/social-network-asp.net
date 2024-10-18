import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import styled from 'styled-components';
import {UserPublicData} from "../../interfaces/models/UserPublicData.ts";
import {getUserDataByUserName} from "../../services/users.ts";

const ProfilePage = () => {
    const { username } = useParams<{ username: string }>();
    const [userProfile, setUserProfile] = useState<UserPublicData | null>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchUserProfile = async () => {
            try {
                const data = await getUserDataByUserName(username!);
                setUserProfile(data);
            } catch (err) {
                setError('Пользователь не найден.');
            }
        };

        void fetchUserProfile();
    }, [username]);

    if (error) {
        return <Container><ErrorMessage><b>{error}</b></ErrorMessage></Container>;
    }

    if (!userProfile) {
        return <Container><p>Загрузка...</p></Container>;
    }

    return (
        <Container>
            <UserName>{userProfile.userName}</UserName>
            <Email>{userProfile.email}</Email>
        </Container>
    );
};

const Container = styled.div`
  margin: 5px;
  width: 100%;
  height: 100%;
  padding: 20px;
  background-color: #cbd8e8;
  border-radius: 15px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
`;

const UserName = styled.h1`
  color: #333;
  font-size: 2.5rem;
  margin-bottom: 10px;
`;

const Email = styled.p`
  color: #555;
  font-size: 1.2rem;
`;

const ErrorMessage = styled.p`
    font-size: 50px;
    color: #dc3545;
`;

export default ProfilePage;
