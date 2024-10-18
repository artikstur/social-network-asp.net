import { useEffect, useState } from "react";
import styled from "styled-components";
import { ChangeUserRolesRequest } from "../interfaces/requests/ChangeUserRolesRequest.ts";
import { User } from "../interfaces/models/User.ts";
import {changeUserRoles, checkAdminAccess, fetchUserRoles, fetchUsers} from "../services/admin.ts";

const ManageUsersPage = () => {
    const [isAdmin, setIsAdmin] = useState<boolean>(false);
    const [users, setUsers] = useState<User[]>([]);
    const [loading, setLoading] = useState(true);
    const [userRoles, setUserRoles] = useState<Record<string, string[]>>({});
    const [error, setError] = useState<string | null>(null);
    const [selectedUser, setSelectedUser] = useState<User | null>(null);
    const [showModal, setShowModal] = useState(false);
    const [roles, _] = useState<string[]>(["User", "Admin"]);
    const [selectedRole, setSelectedRole] = useState<string>(""); // Для одного выбранного значения
    const [isBanned, setIsBanned] = useState(false);

    useEffect(() => {
        const handleIsAdmin = async () => {
            try {
                const adminAccess = await checkAdminAccess();
                setIsAdmin(adminAccess);
            } catch {
                setIsAdmin(false);
            }
            setLoading(false);
        };

        void handleIsAdmin();
    }, []);

    const fetchUsersAndRoles = async () => {
        try {
            const usersData = await fetchUsers();
            setUsers(usersData);

            const rolesPromises = usersData.map(user =>
                fetchUserRoles(user.id).then(roles => ({
                    userId: user.id,
                    roles,
                }))
            );

            const rolesData = await Promise.all(rolesPromises);
            const rolesMap = rolesData.reduce<Record<string, string[]>>((acc, { userId, roles }) => {
                acc[userId] = roles;
                return acc;
            }, {});

            setUserRoles(rolesMap);
        } catch (err) {
            console.log(err);
            setIsAdmin(false);
            setError("Failed to fetch users");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (isAdmin) {
            void fetchUsersAndRoles();
        }
    }, [isAdmin]);

    const handleUserClick = (user: User) => {
        setSelectedUser(user);
        setShowModal(true);
        setSelectedRole(userRoles[user.id]?.[0] || ""); // Установить первую роль как выбранную
        setIsBanned(false);
    };

    const closeModal = () => {
        setShowModal(false);
        setSelectedUser(null);
    };

    const handleRoleChange = (role: string) => {
        setSelectedRole(role);
    };

    const handleSaveRoles = async () => {
        if (!selectedRole) {
            alert("Please select a role!");
            return;
        }

        const requestData: ChangeUserRolesRequest = {
            userId: selectedUser?.id || "",
            roles: [selectedRole],
        };

        try {
            await changeUserRoles(requestData);
            alert("User roles updated successfully");

            await fetchUsersAndRoles();

            closeModal();
        } catch (error) {
            alert("Failed to update roles");
        }
    };

    const toggleBan = () => {
        setIsBanned(prev => !prev);
    };

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    if (!isAdmin) {
        return (
            <NoAccessContainer>
                <Message>У вас недостаточно прав для доступа к этой странице.</Message>
            </NoAccessContainer>
        );
    }

    return (
        <Content>
            <UserList>
                {users.map(user => (
                    <UserName key={user.id} onClick={() => handleUserClick(user)}>
                        {user.userName}
                    </UserName>
                ))}
            </UserList>
            <Statistics>
                <h1>Here is statistics:</h1>
            </Statistics>

            {showModal && selectedUser && (
                <Modal>
                    <ModalContent>
                        <CloseButton onClick={closeModal}>×</CloseButton>
                        <UserInfo>
                            <span>USER INFORMATION</span>
                        </UserInfo>
                        <UserInfo>
                            <span>ID:</span> {selectedUser.id}
                        </UserInfo>
                        <UserInfo>
                            <span>Username:</span> {selectedUser.userName}
                        </UserInfo>
                        <UserInfo>
                            <span>Email:</span> {selectedUser.email}
                        </UserInfo>
                        <RoleChange>
                            <span>Change Role:</span>
                            {roles.map(role => (
                                <Label key={role}>
                                    <input
                                        type="radio"
                                        name="role"
                                        value={role}
                                        checked={selectedRole === role}
                                        onChange={() => handleRoleChange(role)}
                                    />
                                    {role}
                                </Label>
                            ))}
                        </RoleChange>
                        {/* eslint-disable-next-line @typescript-eslint/no-misused-promises */}
                        <SaveButton onClick={handleSaveRoles}>Save Changes</SaveButton>
                        <BanButton
                            onClick={toggleBan}
                            style={{ backgroundColor: isBanned ? 'red' : 'blue' }}
                        >
                            {isBanned ? 'Unban User' : 'Ban User'}
                        </BanButton>
                    </ModalContent>
                </Modal>
            )}
        </Content>
    );
};

// Стили
const NoAccessContainer = styled.div`
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f9f9f9;
`;

const Message = styled.h2`
    color: #dc3545; /* Цвет сообщения об ошибке */
`;

const Content = styled.div`
    padding: 20px;
    width: 100%;
    height: 100%;
    display: flex;
    flex-wrap: wrap;
    gap: 30px;
    background-color: #f9f9f9; 
`;


const Statistics = styled.div`
  flex-grow: 1;
  padding: 20px;
  background-color: #ffffff; 
  border-radius: 10px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
`;

const UserList = styled.div`
  flex-grow: 1;
  height: 100%;
  background-color: #f0f8ff;
  overflow: auto;
  display: flex;
  flex-direction: column;
  gap: 10px;
  border-radius: 10px;
  padding: 10px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
`;

const UserName = styled.div`
  border-radius: 10px;
  background-color: #ffffff; 
  padding: 15px; 
  cursor: pointer;
  font-size: 18px; 
  color: #007bff; 
  transition: background-color 0.3s, transform 0.2s; 

  &:hover {
    background-color: #e7f3ff; 
    transform: translateY(-2px); 
  }
`;

const Modal = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.7); 
  display: flex;
  justify-content: center;
  align-items: center;
`;

const ModalContent = styled.div`
  display: flex;
  flex-direction: column;
  gap: 20px;
  background-color: #ffffff;
  padding: 30px;
  border-radius: 10px;
  max-width: 500px;
  width: 90%; 
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
`;

const CloseButton = styled.button`
  position: absolute;
  top: 15px;
  right: 15px;
  font-size: 24px;
  border: none;
  background: none; 
  cursor: pointer;
  color: #888; 

  &:hover {
    color: #555; /* Темнее при наведении */
  }
`;

const UserInfo = styled.div`
  span {
    font-weight: bold;
    color: #333; 
  }
`;

const RoleChange = styled.div`
  margin-top: 10px;
  padding: 10px; 
  border: 1px solid #ddd; 
  border-radius: 5px; 
  background-color: #f9f9f9;
`;

const Label = styled.label`
  display: block;
  margin: 5px 0;
`;

const SaveButton = styled.button`
  margin-top: 10px;
  background-color: #007bff; 
  color: white;
  border: none;
  padding: 10px;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s; 

  &:hover {
    background-color: #0056b3; 
    transform: translateY(-1px); 
  }
`;

const BanButton = styled.button`
  margin-top: 10px;
  background-color: #dc3545; 
  color: white;
  border: none;
  padding: 10px;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s; 

  &:hover {
    background-color: #c82333; 
    transform: translateY(-1px); 
  }
`;

export default ManageUsersPage;
